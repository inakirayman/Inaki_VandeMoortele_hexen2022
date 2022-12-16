using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameLoop _gameLoop;
    [SerializeField]
    private List<Position> _validPositions = new List<Position>();
    private List<List<Position>> _validPostionGroups = new List<List<Position>>();
    [SerializeField]
    private CardType _type;
    public CardType Type => _type;

    private BoardView _boardView;

    private GameObject _copy;

    public bool IsPlayed = false;
    public bool IsHolding = false;

    public Position selectedPosition;
    private PositionView _positionView;

    private void Start()
    {
        _gameLoop = FindObjectOfType<GameLoop>();
        _boardView = FindObjectOfType<BoardView>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _copy = Instantiate(transform.gameObject, transform.parent);

        _validPositions = new List<Position>();
        _validPostionGroups = new List<List<Position>>();


        if (CardType.Move == Type)
            _validPositions = _gameLoop.GetValidPositions(Type);

        else if(CardType.Shoot == Type || CardType.Slash == Type || CardType.ShockWave == Type )
        {
            _validPostionGroups = _gameLoop.GetValidPositionsOptions(Type);

            foreach (List<Position> positions in _validPostionGroups)
            {

                foreach (Position position in positions)
                {
                    _validPositions.Add(position);
                }
            }


            
        }
            
        IsHolding = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _copy.transform.position = eventData.position;



        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Tile")
        {
            PositionView positionView = hit.transform.gameObject.GetComponent<PositionView>();


            if(CardType.Move == Type)
                MoveCardHighlightFunction(positionView);

            else if (CardType.Shoot == Type || CardType.Slash == Type || CardType.ShockWave == Type)
                CardHighlightGroup(positionView);

        }
        else if (_positionView != null)
        {
            _positionView.DeActivate();
        }
        else
            _boardView.SetActivePosition = new List<Position>();

    }

    private void CardHighlightGroup(PositionView positionView)
    {
        if (!_validPositions.Contains(positionView.HexPosition))
            _boardView.SetActivePosition = _validPositions;
        else
            foreach (List<Position> positions in _validPostionGroups)
            {

                if (positions.Contains(positionView.HexPosition) && CardType.Shoot == Type)
                {
                    _boardView.SetActivePosition = positions;
                    break;
                }
                else if (positions[1].Q == positionView.HexPosition.Q && positions[1].R == positionView.HexPosition.R && CardType.Slash == Type )
                {
                    _boardView.SetActivePosition = positions;
                    break;
                }
                else if (positions[1].Q == positionView.HexPosition.Q && positions[1].R == positionView.HexPosition.R && CardType.ShockWave == Type)
                {
                    _boardView.SetActivePosition = positions;
                    break;
                }


            }

    }

    private void MoveCardHighlightFunction(PositionView positionView)
    {
        if (_validPositions.Contains(positionView.HexPosition))
        {
            if (selectedPosition.Q != positionView.HexPosition.Q || selectedPosition.R != positionView.HexPosition.R && _positionView != null)
            {
                if (_positionView != null)
                    _positionView.DeActivate();
            }

            positionView.Activate();
            _positionView = positionView;
            selectedPosition = _positionView.HexPosition;
        }
        else if (_positionView != null)
            _positionView.DeActivate();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_positionView != null)
        _positionView.DeActivate();
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Tile")
        {

            PositionView positionView = hit.transform.gameObject.GetComponent<PositionView>();
            selectedPosition = positionView.HexPosition;
            Destroy(_copy);
            IsPlayed = true;
            positionView.OnPointerClick(eventData);
            
            

        }
        else
            Destroy(_copy);

        IsHolding = false;

        _boardView.SetActivePosition = new List<Position>();
    }


}

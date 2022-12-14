using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameLoop _gameLoop;
    [SerializeField]
    private List<Position> _validPositions = new List<Position>();
    [SerializeField]
    private CardType _type;
    public CardType Type => _type;


    private GameObject _copy;

    public bool IsPlayed = false;
    public bool IsHolding = false;

    public Position selectedPosition;
    private PositionView _positionView;

    private void Start()
    {
        _gameLoop = FindObjectOfType<GameLoop>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _copy = Instantiate(transform.gameObject, transform.parent);
        _validPositions = _gameLoop.GetValidPositions(Type);
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

        }
        else if (_positionView != null)
            _positionView.DeActivate();
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

    }


}

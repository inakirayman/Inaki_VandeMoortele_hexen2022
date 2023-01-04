using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine 
{


    private List<Position> _selectedPositions = new List<Position>();
    public List<Position> SelectedPositions => _selectedPositions;

    private  Board _board;
    private  PieceView _player;
    private  Deck _deck;
    private  PieceView[] _pieces;
    private  BoardView _boardView;
    

    public Engine(Board board ,BoardView boardView, PieceView player, Deck deck,PieceView[] pieces)
    {
        _board = board;
        _player = player;
        _deck = deck;
        _pieces = pieces;
        _boardView = boardView;
    }

    public void CardLogic(Position position)
    {
        var cards = _deck.GetComponentsInChildren<Card>();
        foreach (Card card in cards)
        {

            if (card.IsPlayed)
            {

                if (card.Type == CardType.Move)
                {
                    card.IsPlayed = _board.Move(PositionHelper.WorldToHexPosition(_player.WorldPosition), position);

                }
                else if (!_selectedPositions.Contains(position))
                {
                    card.IsPlayed = false;
                    return;
                }
                else if (card.Type == CardType.Slash)
                {

                    foreach(Position pos in _selectedPositions)
                    {
                        _board.Take(pos);
                    }
                }
                else if (card.Type == CardType.Shoot)
                {
                    foreach (Position pos in _selectedPositions)
                    {
                        _board.Take(pos);
                    }
                }
                else if (card.Type == CardType.ShockWave)
                {

                    foreach(Position pos in _selectedPositions)
                    {
                        Position offset = HexHelper.AxialSubtract(pos, PositionHelper.WorldToHexPosition(_player.WorldPosition));
                        Position moveTo = HexHelper.AxialAdd(pos, offset);

                        if (_board.IsValidPosition(moveTo))
                        {
                            _board.Move(pos, moveTo);
                        }
                        else
                        _board.Take(pos);
                        

                    }


                }

            }

        }

        _deck.DeckUpdate();
    }

    public void SetHighlights(Position position, CardType type, List<Position> validPositions, List<List<Position>> validPositionGroups = null )
    {

        if (CardType.Move == type)
        {
            List<Position> positions = new List<Position>();

            if (validPositions.Contains(position))
            {
                positions.Add(position);

                SetActiveTiles(positions);
            }
        }
        else if (CardType.Shoot == type || CardType.Slash == type || CardType.ShockWave == type)
        {

            if (!validPositions.Contains(position))
                SetActiveTiles(validPositions);
            else
                foreach (List<Position> positions in validPositionGroups)
                {

                    if (positions.Count == 0) ;

                    else if (CardType.Shoot == type && positions.Contains(position))
                    {
                        SetActiveTiles(positions);
                        _selectedPositions = positions;
                        break;
                    }
                    else if (CardType.Slash == type && positions[0] == position)
                    {
                        SetActiveTiles(positions);
                        _selectedPositions = positions;
                        break;
                    }
                    else if (CardType.ShockWave == type && positions[0] == position)
                    {
                        SetActiveTiles(positions);
                        _selectedPositions = positions;
                        break;
                    }


                }
        }
        else
            _selectedPositions = new List<Position>();
    }
    public void SetActiveTiles(List<Position> positions)
    {
        _boardView.SetActivePosition = positions;
    }

    public List<Position> GetValidPositions(CardType card)
    {
        List<Position> positions = new List<Position>();

        if (card == CardType.Move)
        {

            foreach (var position in _boardView.TilePositions)
            {
                bool positionIsFree = true;

                foreach (var piece in _pieces)
                {
                    var pos = PositionHelper.WorldToHexPosition(piece.WorldPosition);
                    if (pos.Q == position.Q && pos.R == position.R && piece.gameObject.activeSelf)
                    {
                        positionIsFree = false;
                        break;
                    }
                }
                if (positionIsFree)
                {
                    positions.Add(position);
                }
            }


            return positions;
        }



        return null;
    }

    public List<List<Position>> GetValidPositionsGroups(CardType card)
    {
        if (card == CardType.Shoot)
        {
            return MoveSetCollection.GetValidTilesForShoot(_player, _board);
        }
        else if (card == CardType.Slash || card == CardType.ShockWave)
        {
            return MoveSetCollection.GetValidTilesForCone(_player, _board);
        }

        return null;
    }

}

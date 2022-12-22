using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    private Board _board;
    private Deck _deck;
    private BoardView _boardView;
    private PieceView Player1;
    private Engine _engine;
    private PieceView[] _pieces;


    void Start()
    {
        
        SpawnHelper.SpawnEnemies(_enemy, 8);

        _deck = FindObjectOfType<Deck>();

        _board = new Board(PositionHelper.Distance);
        _board.PieceMoved += (s, e)
             => e.Piece.MoveTo(PositionHelper.HexToWorldPosition(e.ToPosition));

        _board.PieceTaken += (s, e)
            => e.Piece.Taken();

        _board.PiecePlaced += (s, e)
           => e.Piece.Placed(PositionHelper.HexToWorldPosition(e.ToPosition));

        var piecesViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in piecesViews)
            _board.Place(PositionHelper.WorldToHexPosition(pieceView.WorldPosition), pieceView);
        PieceView player =null;
        foreach (var pieceView in piecesViews)
         if (pieceView.Player == Player.Player1)
         {
            player = pieceView;
            break;
         }
       

        _pieces = piecesViews;


        
        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
        _boardView = boardView;

        _engine = new Engine(_board, _boardView, player, _deck, _pieces);

        _deck.SetupCards(_engine);


    }

    //public List<Position> GetValidPositions(CardType card)
    //{
    //    List<Position> positions = new List<Position>();

    //    if(card == CardType.Move)
    //    {

    //        foreach (var position in _boardView.TilePositions)
    //        {
    //            bool positionIsFree = true;

    //            foreach (var piece in _pieces)
    //            {
    //                var pos = PositionHelper.WorldToHexPosition(piece.WorldPosition);
    //                if (pos.Q == position.Q && pos.R == position.R && piece.gameObject.activeSelf)
    //                {
    //                    positionIsFree = false;
    //                    break;
    //                }
    //            }

    //            if (positionIsFree)
    //            {
    //                positions.Add(position);
    //            }
    //        }


    //        return positions;
    //    }
       


    //    return null;
    //}

    //public List<List<Position>> GetValidPositionsGroups(CardType card)
    //{
    //    if (card == CardType.Shoot)
    //    {
    //        return MoveSetCollection.GetValidTilesForShoot(Player1, _board);
    //    }
    //    else if(card == CardType.Slash || card == CardType.ShockWave)
    //    {
    //        return MoveSetCollection.GetValidTilesForCone(Player1, _board);
    //    }

    //    return null;
    //}


    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
       
        _engine.CardLogic(e.Position);


        Debug.Log(e.Position);
    }














}

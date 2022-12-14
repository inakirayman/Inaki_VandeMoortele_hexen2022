using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Board _board;
    private Deck _deck;

    private PieceView Player1;
    [SerializeField]
    private GameObject _enemy;

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
        foreach (var pieceView in piecesViews)
         if (pieceView.Player == Player.Player1)
         {
            Player1 = pieceView;
            break;
         }
        
            

        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
       
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        var cards = _deck.GetComponentsInChildren<Card>();
        foreach(Card card in cards)
        {

            if (card.IsPlayed)
            {
                
                if(card.Type == CardType.Move)
                {
                    card.IsPlayed = _board.Move(PositionHelper.WorldToHexPosition(Player1.WorldPosition), e.Position);
                    
                }
                else if (card.Type == CardType.Slash)
                {

                }
                else if (card.Type == CardType.Lazer)
                {

                }
                else if (card.Type == CardType.ShockWave)
                {

                }

            }

        }
        _deck.DeckUpdate();

    }













}

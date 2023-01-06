using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayingState : State
{
    [SerializeField]
    private GameObject _enemy;
    private Board _board;
    private Deck _deck;
    private BoardView _boardView;
    private PieceView Player1;
    private Engine _engine;
    private PieceView[] _pieces;

    public override void OnEnter()
    {
        base.OnEnter();


        var op = SceneManager.LoadSceneAsync("HexenGame", LoadSceneMode.Additive);

        op.completed += InitializeScene;
    }

    private void InitializeScene(AsyncOperation obj)
    {
        SpawnHelper.SpawnEnemies(8);

        _deck = GameObject.FindObjectOfType<Deck>();

        _board = new Board(PositionHelper.Distance);
        _board.PieceMoved += (s, e)
             => e.Piece.MoveTo(PositionHelper.HexToWorldPosition(e.ToPosition));

        _board.PieceTaken += (s, e)
            => e.Piece.Taken();

        _board.PiecePlaced += (s, e)
           => e.Piece.Placed(PositionHelper.HexToWorldPosition(e.ToPosition));

        var piecesViews = GameObject.FindObjectsOfType<PieceView>();

        foreach (var pieceView in piecesViews)
            _board.Place(PositionHelper.WorldToHexPosition(pieceView.WorldPosition), pieceView);

        PieceView player = null;
        foreach (var pieceView in piecesViews)
            if (pieceView.Player == Player.Player1)
            {
                player = pieceView;
                break;
            }


        _pieces = piecesViews;



        var boardView = GameObject.FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
        _boardView = boardView;

        _engine = new Engine(_board, _boardView, player, _deck, _pieces);

        _deck.SetupCards(_engine);
    }

    public override void OnExit()
    {
        base.OnExit();

        if (_boardView != null)
            _boardView.PositionClicked -= OnPositionClicked;

        SceneManager.UnloadSceneAsync("HexenGame");
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        _engine.CardLogic(e.Position);

        Debug.Log(e.Position);
    }

}

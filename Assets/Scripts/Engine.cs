using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private readonly Board _board;

    private readonly PieceView _player;

    public Engine(Board board , PieceView player)
    {
        _board = board;
        _player = player;
    }


}

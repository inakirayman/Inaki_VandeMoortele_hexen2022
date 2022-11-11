using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;
        
    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        Debug.Log(e.Position);
        Debug.Log(PositionHelper.HexToWorldPosition(e.Position));
    }
}

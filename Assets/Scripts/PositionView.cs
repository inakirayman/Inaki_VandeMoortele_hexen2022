using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView : MonoBehaviour, IPointerClickHandler
{
    private BoardView _parent;

    public Position HexPosition => PositionHelper.WorldToHexPosition(transform.position);


    private void Start()
    {
        _parent = GetComponentInParent<BoardView>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _parent.ChildClicked(this);
    }

}

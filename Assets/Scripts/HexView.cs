using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexView : MonoBehaviour, IPointerClickHandler
{

    public Hex HexPosition => PositionHelper.WorldToHexPosition(transform.position);

    public event EventHandler Clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClicked(EventArgs.Empty);
        Debug.Log(HexPosition);
    }

    protected virtual void onClicked(EventArgs eventArgs)
    {
        var handler = Clicked;
        handler?.Invoke(this, eventArgs);
    }

}

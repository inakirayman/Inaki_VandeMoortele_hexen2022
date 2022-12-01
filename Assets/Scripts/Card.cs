using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField]
    private CardType _type;

    private GameObject _copy;

    public bool IsPlayed = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _copy = Instantiate(transform.gameObject, transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _copy.transform.position = eventData.position;

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Tile")
        {
            Debug.Log("HIT");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == "Tile")
        {

            hit.transform.gameObject.GetComponent<PositionView>().OnPointerClick(eventData);
            Destroy(_copy);
            IsPlayed = true;
            gameObject.SetActive(false);

        }
        else
            Destroy(_copy);

    }


}

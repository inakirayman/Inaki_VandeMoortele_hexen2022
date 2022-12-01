using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField]
    private static int _deckSize = 12;

    [SerializeField]
    private int _handSize = 5;

    [SerializeField]
    private GameObject[] _cards;

    [SerializeField]
    private GameObject[] _cardPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _deckSize ; i++)
        {
            GameObject card = Instantiate(_cardPrefabs[Random.Range(0, _cardPrefabs.Length)], transform);
            card.transform.gameObject.SetActive(false);
            _cards[i] = card;
        }
        Debug.Log("Deck Generated");
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> tmp = new List<GameObject>(_cards);

        Vector3 startPosition = transform.position;
        startPosition = GetStartPosition(tmp, transform.position);

        int handSize;
        if (tmp.Count >= 5)
            handSize = _handSize;
        else
            handSize = tmp.Count;



        for (int i = 0; i < handSize; i++)
        {
            GameObject card = tmp[i];
            card.SetActive(true);
            card.transform.position = startPosition;

            startPosition += new Vector3(60, 0);
        }

        for (int i = 0; i < tmp.Count; i++)
        {
            GameObject card = tmp[i];

            if (card.GetComponent<Card>().IsPlayed)
            {
                tmp.RemoveAt(i);
                card.SetActive(false);
            }

        }

        _cards = tmp.ToArray();

    }

    private Vector3 GetStartPosition(List<GameObject> tmp, Vector3 startPosition)
    {
        if (tmp.Count >= 5)
            startPosition = transform.position + new Vector3(-120, 0, 0);

        else if (tmp.Count == 4)
            startPosition = transform.position + new Vector3(-90, 0, 0);

        else if (tmp.Count == 3)
            startPosition = transform.position + new Vector3(-60, 0, 0);

        else if (tmp.Count == 2)
            startPosition = transform.position + new Vector3(-30, 0, 0);

        else if (tmp.Count == 1)
            startPosition = transform.position;
        return startPosition;
    }
}

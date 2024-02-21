using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{

    [SerializeField]
    private GameObject _playerHand;
    [SerializeField]
    private GameObject _playerBoard;
    [SerializeField]
    private GameObject _cardPrefab;
    [SerializeField]
    private List<Card> _cards = new List<Card>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AddCardToHand();
    }

    private void AddCardToHand()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            GameObject cardInstance = Instantiate(_cardPrefab,_playerHand.transform,true);

            cardInstance.GetComponent<CardDisplay>()._card = _cards[Random.Range(0, _cards.Count)];

            cardInstance.GetComponent<Transform>().transform.localScale = new Vector3(1,1,1);

            cardInstance.transform.SetParent(_playerHand.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Game
{
    public List<Card> enemyDeck, playerDeck, enemyHand, playerHand, enemyField, playerField;

    public Game()
    {
        enemyDeck = GiveDeckCard();
        playerDeck = GiveDeckCard();

        enemyHand = new List<Card>();
        playerHand = new List<Card>();

        enemyField = new List<Card>();
        playerField = new List<Card>();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for(int i = 0; i < 10; i ++)
        {
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        }
        return list;
    }
}

public class GameManagerScript : MonoBehaviour
{
    public Game currentGame;
    public Transform enemyHand, playerHand;
    public GameObject cardPref;

    private void Start()
    {
        currentGame = new Game();

        GiveStartCards(currentGame.enemyDeck,enemyHand);
        GiveStartCards(currentGame.playerDeck, playerHand);
    }

    void GiveStartCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(deck,hand);
    }

    void GiveCardToHand(List<Card> deck , Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card card = deck[0];

        GameObject cardGO = Instantiate(cardPref, hand, false);

        if (hand == enemyHand)
            cardGO.GetComponent<CardInfoScript>().HideCardInfo(card);
        else
            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card);

        deck.RemoveAt(0);
    }
}

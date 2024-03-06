using Script.Card;
using Script.Card.CardDeck;
using Script.Characters.Player;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Networking
{
    public class NetworkPlayerSpawnerCards : MonoBehaviour
    {
        public GameObject cardPref;
        [SerializeField]public List<CardInfoDisplay> Board = new List<CardInfoDisplay>();
        public bool IsPlayer;
        private const int _maxPlayerHandSize = 6;
        public Transform PlayerHand;

        private NetworkPlayerDeck CurrentPlayerCardDeckInstance;

        public List<CardInfoDisplay> PlayerHandCards = new List<CardInfoDisplay>();

        

        void Start()
        {
            CurrentPlayerCardDeckInstance = new NetworkPlayerDeck();
            GiveStartCards(CurrentPlayerCardDeckInstance.PlayerDeck, PlayerHand);
            IsPlayer = true;
        }
        protected void GiveStartCards(List<Card.Card> deck, Transform hand)
        {
            int i = 0;
            while (i++ < 4)
                GiveCardToHand(deck, hand);
        }


        public void GiveCardToHand(List<Card.Card> deck, Transform hand)
        {
            if (deck.Count == 0)
                return;
            if (LogAndBurnCardsIfHandIsFull(deck, hand))
                return;

            Card.Card characterCard = deck.FirstOrDefault();
            if (characterCard == null)
                return;

            SetupCard(characterCard, hand);

            deck.Remove(characterCard);
        }
        public void GiveNewCards()
        {
            CurrentPlayerCardDeckInstance ??= new NetworkPlayerDeck();
            GiveCardToHand(CurrentPlayerCardDeckInstance.PlayerDeck, PlayerHand);
        }
        protected void SetupCard(Card.Card characterCard, Transform hand)
        {
            GameObject cardGameObj = Instantiate(cardPref, hand, false);
            cardGameObj.name = characterCard.name;
            
            CardInfoDisplay cardInfoDisplay = cardGameObj.GetComponent<CardInfoDisplay>();
            cardInfoDisplay.OwnerHp = GetComponent<PlayerHealth>();
            cardInfoDisplay.NetworkPlayerSpawnerCards = this;

            if (hand == PlayerHand)
            {
                cardInfoDisplay.ShowCardInfo(characterCard, true);
                PlayerHandCards.Add(cardInfoDisplay);
            }
        }
        protected bool LogAndBurnCardsIfHandIsFull(List<Card.Card> deck, Transform hand)
        {
            if (hand == PlayerHand && PlayerHandCards.Count >= _maxPlayerHandSize)
            {
                LogAndBurnCard(deck, "Player's hand is full. Burning card: ");
                return true;
            }
            return false;
        }
        protected void LogAndBurnCard(List<Card.Card> deck, string message)
        {
            Card.Card characterCard = deck.FirstOrDefault();
            if (characterCard != null)
            {
                Debug.Log(message + characterCard.name);
                deck.Remove(characterCard);
            }   
        }
    }
}

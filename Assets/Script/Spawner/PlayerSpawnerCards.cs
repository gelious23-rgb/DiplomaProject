using Script.Card;
using Script.Card.CardDeck;
using System.Collections.Generic;
using System.Linq;
using Script.Characters.Player;
using UnityEngine;

namespace Script.Spawner
{
    public class PlayerSpawnerCards : SpawnerCards
    {
        private const int _maxPlayerHandSize = 6;
        public Transform PlayerHand;

        private PlayerCardDeckInstance CurrentPlayerCardDeckInstance;

        public List<CardInfoDisplay> PlayerHandCards = new List<CardInfoDisplay>(),
            PlayerFieldCards = new List<CardInfoDisplay>();

        void Start()
        {
            CurrentPlayerCardDeckInstance = new PlayerCardDeckInstance();
            GiveStartCards(CurrentPlayerCardDeckInstance.PlayerDeck, PlayerHand);
        }
        protected override void GiveStartCards(List<Card.Card> deck, Transform hand)
        {
            int i = 0;
            while (i++ < 4)
                GiveCardToHand(deck, hand);
        }


        protected override void GiveCardToHand(List<Card.Card> deck, Transform hand)
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
        public override void GiveNewCards()
        {
            CurrentPlayerCardDeckInstance ??= new PlayerCardDeckInstance();
            GiveCardToHand(CurrentPlayerCardDeckInstance.PlayerDeck, PlayerHand);
        }
        protected override void SetupCard(Card.Card characterCard, Transform hand)
        {
            GameObject cardGameObj = Instantiate(cardPref);
            cardGameObj.transform.SetParent(hand, false);
            
            CardInfoDisplay cardInfoDisplay = cardGameObj.GetComponent<CardInfoDisplay>();
            cardInfoDisplay.owner = GetComponent<PlayerHealth>();

            if (hand == PlayerHand)
            {
                cardInfoDisplay.ShowCardInfo(characterCard, true);
                PlayerHandCards.Add(cardInfoDisplay);
            }
        }
        protected override bool LogAndBurnCardsIfHandIsFull(List<Card.Card> deck, Transform hand)
        {
            if (hand == PlayerHand && PlayerHandCards.Count >= _maxPlayerHandSize)
            {
                LogAndBurnCard(deck, "Player's hand is full. Burning card: ");
                return true;
            }
            return false;
        }
        protected override void LogAndBurnCard(List<Card.Card> deck, string message)
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

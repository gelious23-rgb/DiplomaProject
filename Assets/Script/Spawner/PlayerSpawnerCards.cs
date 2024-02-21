using Script.Card;
using Script.Card.CardDeck;
using System.Collections.Generic;
using System.Linq;
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
        protected override void GiveStartCards(List<CharacterCard> deck, Transform hand)
        {
            int i = 0;
            while (i++ < 4)
                GiveCardToHand(deck, hand);
        }


        protected override void GiveCardToHand(List<CharacterCard> deck, Transform hand)
        {
            if (deck.Count == 0)
                return;
            if (LogAndBurnCardsIfHandIsFull(deck, hand))
                return;

            CharacterCard characterCard = deck.FirstOrDefault();
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
        protected override void SetupCard(CharacterCard characterCard, Transform hand)
        {
            GameObject cardGameObj = Instantiate(cardPref);
            cardGameObj.transform.SetParent(hand, false);
            
            CardInfoDisplay cardInfoDisplay = cardGameObj.GetComponent<CardInfoDisplay>();

            if (hand == PlayerHand)
            {
                cardInfoDisplay.ShowCardInfo(characterCard, true);
                PlayerHandCards.Add(cardInfoDisplay);
            }
        }
        protected override bool LogAndBurnCardsIfHandIsFull(List<CharacterCard> deck, Transform hand)
        {
            if (hand == PlayerHand && PlayerHandCards.Count >= _maxPlayerHandSize)
            {
                LogAndBurnCard(deck, "Player's hand is full. Burning card: ");
                return true;
            }
            return false;
        }
        protected override void LogAndBurnCard(List<CharacterCard> deck, string message)
        {
            CharacterCard characterCard = deck.FirstOrDefault();
            if (characterCard != null)
            {
                Debug.Log(message + characterCard.Name);
                deck.Remove(characterCard);
            }   
        }
    }
}

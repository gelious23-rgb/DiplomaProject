﻿using Script.Card;
using Script.Card.CardDeck;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Spawner
{
    public class EnemySpawnerCards : SpawnerCards
    {
        [SerializeField]private Transform EnemyHand;
        private const int _maxEnemyHandSize = 6;
        private EnemyCardDeckInstance CurrentEnemyCardDeckInstance;
        public List<CardInfoDisplay> EnemyHandCards = new List<CardInfoDisplay>(),
            EnemyFieldCards = new List<CardInfoDisplay>();

        void Start()
        {
            CurrentEnemyCardDeckInstance = new EnemyCardDeckInstance();
            GiveStartCards(CurrentEnemyCardDeckInstance.EnemyDeck, EnemyHand);
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
            CurrentEnemyCardDeckInstance ??= new EnemyCardDeckInstance();
            GiveCardToHand(CurrentEnemyCardDeckInstance.EnemyDeck, EnemyHand);
        }
        protected override void SetupCard(CharacterCard characterCard, Transform hand)
        {
            GameObject cardGameObj = Instantiate(cardPref, hand, false);

            CardInfoDisplay cardInfoDisplay = cardGameObj.GetComponent<CardInfoDisplay>();

            if (hand == EnemyHand)
            {
                cardInfoDisplay.HideCardInfo(characterCard);
                EnemyHandCards.Add(cardInfoDisplay);
            }
        }
        protected override bool LogAndBurnCardsIfHandIsFull(List<CharacterCard> deck, Transform hand)
        {
            if (hand == EnemyHand && EnemyHandCards.Count >= _maxEnemyHandSize)
            {
                LogAndBurnCard(deck, "Enemy's hand is full. Burning card: ");
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
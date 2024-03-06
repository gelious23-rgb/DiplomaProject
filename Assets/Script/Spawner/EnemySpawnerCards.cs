﻿using Script.Card;
using Script.Card.CardDeck;
using System.Collections.Generic;
using System.Linq;
using Script.Characters.Enemy;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Spawner
{
    public class EnemySpawnerCards : SpawnerCards
    {
        [SerializeField] internal Transform EnemyHand;
        [SerializeField] internal Transform PlayerHand;
        private const int _maxEnemyHandSize = 6;
        private EnemyCardDeckInstance CurrentEnemyCardDeckInstance;

        public List<CardInfoDisplay> EnemyHandCards = new List<CardInfoDisplay>();


        public void StartGame()
        {
            CurrentEnemyCardDeckInstance = new EnemyCardDeckInstance();
            var hand = NetworkManager.Singleton.IsHost ? EnemyHand : PlayerHand;
            GiveStartCards(CurrentEnemyCardDeckInstance.EnemyDeck, hand);
            IsPlayer = false;
        }
        
        protected override void GiveStartCards(List<Card.Card> deck, Transform hand)
        {
            int i = 0;
            while (i++ < 4)
                GiveCardToHand(deck, hand);
            
        }


        public override void GiveCardToHand(List<Card.Card> deck, Transform hand)
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
            CurrentEnemyCardDeckInstance ??= new EnemyCardDeckInstance();
            GiveCardToHand(CurrentEnemyCardDeckInstance.EnemyDeck, EnemyHand);
        }
        protected override void SetupCard(Card.Card characterCard, Transform hand)
        {
            GameObject cardGameObj = Instantiate(cardPref);
            cardGameObj.name = characterCard.name;
            CardInfoDisplay cardInfoDisplay = cardGameObj.GetComponent<CardInfoDisplay>();
            cardInfoDisplay.PlayerHand = PlayerHand;
            cardInfoDisplay.EnemyHand = EnemyHand;
            cardInfoDisplay.IsPlayer = false;
            cardInfoDisplay.OwnerHp = GetComponent<EnemyHealth>();
            cardInfoDisplay.owner = this;

            cardGameObj.GetComponent<NetworkObject>()
                .SpawnWithOwnership(NetworkManager.Singleton.LocalClient.ClientId, true);
            if (hand == EnemyHand)
            {
                cardInfoDisplay.HideCardInfoClientRpc(characterCard);
                EnemyHandCards.Add(cardInfoDisplay);
            }
        }
        protected override bool LogAndBurnCardsIfHandIsFull(List<Card.Card> deck, Transform hand)
        {
            if (hand == EnemyHand && EnemyHandCards.Count >= _maxEnemyHandSize)
            {
                LogAndBurnCard(deck, "Enemy's hand is full. Burning card: ");
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
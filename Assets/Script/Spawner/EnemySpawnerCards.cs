using Script.Card;
using Script.Card.CardDeck;
using System.Collections.Generic;
using System.Linq;
using Script.Characters.Enemy;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Spawner
{
    public class EnemySpawnerCards : SpawnerCards
    {
        [SerializeField] internal Transform EnemyHand;
        [SerializeField] internal Transform PlayerHand;
        private const int _maxEnemyHandSize = 6;

        public List<Card.Card> EnemyDeck;
        public List<CardInfoDisplay> EnemyHandCards = new List<CardInfoDisplay>();
        [Space]
        public Image EnemyBoardImage;
        public Sprite HeavensBoard;
        public Sprite HellBoard;


        public void StartGame(List<int> enemyPlayerDeck)
        {
            var allCards = new EnemyCardDeckInstance().GetCardLibrary();
            EnemyDeck = enemyPlayerDeck.Select(index => allCards.AllCards[index]).ToList();
            var hand = NetworkManager.Singleton.IsHost ? EnemyHand : PlayerHand;
            GiveStartCards(EnemyDeck, EnemyHand);
            IsPlayer = false;
            
            EnemyBoardImage.sprite = NetworkManager.Singleton.IsHost ? HellBoard : HeavensBoard;
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
            GiveCardToHand(EnemyDeck, EnemyHand);
        }
        protected override void SetupCard(Card.Card characterCard, Transform hand)
        {
            GameObject cardGameObj = Instantiate(cardPref);
            cardGameObj.transform.SetParent(hand, false);
            cardGameObj.name = characterCard.name;
            CardInfoDisplay cardInfoDisplay = cardGameObj.GetComponent<CardInfoDisplay>();
            cardInfoDisplay.PlayerHand = PlayerHand;
            cardInfoDisplay.EnemyHand = EnemyHand;
            cardInfoDisplay.IsPlayer = false;
            cardInfoDisplay.OwnerHp = GetComponent<EnemyHealth>();
            cardInfoDisplay.owner = this;
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

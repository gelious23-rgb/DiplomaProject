using System.Collections.Generic;
using Script.Card;
using Script.Characters;
using UnityEngine;


namespace Script.Game
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private int _playerDeckCardCount;
        [SerializeField] private int _enemyDeckCardCount;
        [SerializeField] private int _playerHandCardLimitCount;
        [SerializeField] private int _enemyHandCardLimitCount;
        [SerializeField] private int _boardCardLimitCount;

        [SerializeField] private GameObject _canvas;

        [SerializeField] private Player _player;
        [SerializeField] private GameObject _playerHand;
        [SerializeField] private GameObject _playerBoard;
        [SerializeField] private GameObject _enemyHand;

        [SerializeField] private GameObject _enemyBoard;

        public CardDisplay CardPrefab;

        [SerializeField] private Transform _cardInstanceTransform;
        [SerializeField] private List<Card.Card> _cards = new();
        [SerializeField] private List<GameObject> _playerDeck;
        [SerializeField] private List<GameObject> _enemyDeck;
        [SerializeField] private Mana.Mana _mana;

        private void Start()
        {
            InitializePlayerDeck();
            InitializeEnemyDeck();
        }

        public GameObject EnemyHand()
        {
            return _enemyHand;
        }

        public GameObject EnemyBoard()
        {
            return _enemyBoard;
        }

        public void AddCardToPlayerHand(int cardAmount)
        {
            for (var i = 0; i < cardAmount; i++)
            {
                if (_playerHand.transform.childCount < _playerHandCardLimitCount)
                {
                    var card = _playerDeck[0];
                    _playerDeck.Remove(_playerDeck[0]);
                    card.transform.SetParent(_playerHand.transform);
                }
                else
                {
                    // Burn the card if the hand is full
                    var card = _playerDeck[0];
                    _playerDeck.Remove(_playerDeck[0]);
                    Destroy(card);
                    Debug.Log("Player's card burned!");
                }
            }
        }

        public void AddCardToEnemyHand(int cardAmount)
        {
            for (var i = 0; i < cardAmount; i++)
            {
                if (_enemyHand.transform.childCount < _enemyHandCardLimitCount)
                {
                    var card = _enemyDeck[0];
                    _enemyDeck.Remove(_enemyDeck[0]);
                    card.transform.SetParent(_enemyHand.transform);
                }
                else
                {
                    // Burn the card if the hand is full
                    var card = _enemyDeck[0];
                    _enemyDeck.Remove(_enemyDeck[0]);
                    Destroy(card);
                    Debug.Log("Enemy's card burned!");
                }
            }
        }

        public void InitializePlayerDeck()
        {
            for (var i = 0; i < _playerDeckCardCount; i++) _playerDeck.Add(CreateRandomCard());
        }

        public void InitializeEnemyDeck()
        {
            for (var i = 0; i < _enemyDeckCardCount; i++) _enemyDeck.Add(CreateRandomCard());
        }

        private GameObject CreateRandomCard()
        {
            var cardInstance = Instantiate(CardPrefab, _cardInstanceTransform);

            var cardData = _cards[Random.Range(0, _cards.Count)];
            cardInstance.Card = cardData;

            var cardDragDrop = cardInstance.gameObject.AddComponent<CardDragAndDrop>();
            cardDragDrop.Initialize(_playerHand, _playerBoard, _enemyHand, _enemyBoard, _canvas, _boardCardLimitCount, _player, _mana,
                cardData.Manacost);

            return cardInstance.gameObject;
        }
    }
}
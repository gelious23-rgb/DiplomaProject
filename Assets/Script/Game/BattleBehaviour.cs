using System.Collections;
using Script.Card;
using Script.Characters;
using Script.UI.Buttons;
using Script.UI.Panel;
using UnityEngine;


namespace Script.Game
{
    public class BattleBehaviour : MonoBehaviour
    {
        public Player HumanPlayer;
        public BotPlayer BotPlayer;
        private Player _activePlayer;
        
        [SerializeField]
        private BoardManager _boardManager;
        [SerializeField]
        private NextTurnButton _nextTurnButton;
        private WinPanel _winPanel;
        private LosePanel _losePanel;
        
        private bool _playerTurnSkipped;
        public float TurnTimeLimit = 30f;
        private float _turnTimer;
        
        
        private void Start()
        {
            _boardManager.AddCardToPlayerHand(3);
            _boardManager.AddCardToEnemyHand(3);
            _activePlayer = HumanPlayer;
            StartTurn();
        }
        
        private void StartTurn()
         {
             _activePlayer.StartTurn();
             _playerTurnSkipped = false;
             _turnTimer = TurnTimeLimit;

             if (_activePlayer == HumanPlayer)
             {
                 _nextTurnButton.gameObject.SetActive(true);
                 StartCoroutine(TurnPlayer());
             }
             else
                 StartCoroutine(TurnBot());
         }
        
        public void SkipPlayerTurn()
        {
            if (_activePlayer == HumanPlayer)
            {
                _playerTurnSkipped = true;
                _nextTurnButton.gameObject.SetActive(false);
                EndTurn();
            }
        }

        public IEnumerator TurnBot()
        {
            _boardManager.AddCardToEnemyHand(1);
            yield return new WaitForSeconds(1f);

            int cardsOnBoard = _boardManager.EnemyBoard().gameObject.transform.childCount;

            for (int i = 0; i < _boardManager.EnemyHand().gameObject.transform.childCount && cardsOnBoard < 5; i++)
            {
                int currentCardManacost = _boardManager.EnemyHand().gameObject.transform.GetChild(i).gameObject.GetComponent<CardDisplay>().Card.Manacost;

                if (currentCardManacost <= GetEnemyCurrentMana())
                {
                    _boardManager.EnemyHand().gameObject.transform.GetChild(i).gameObject.transform.SetParent(_boardManager.EnemyBoard().transform);
                    BotPlayer.ManaObject.Decrease(currentCardManacost);

                    // Increment cardsOnBoard count since a card has been placed on the board
                    cardsOnBoard++;
                }
            }
            BotPlayer.ManaObject.ManaCurrent = BotPlayer.ManaObject.ManaMax;
            yield return new WaitForSeconds(1f);
            EndTurn();
        }

        public IEnumerator TurnPlayer()
        {
             _boardManager.AddCardToPlayerHand(1);
             yield return TurnTimer();

             if (_activePlayer.ShouldSkipTurn())
                 Debug.Log(_activePlayer.PlayerName + " skipped their turn.");
             else if (_turnTimer <= 0f) Debug.Log(_activePlayer.PlayerName + " ran out of time.");
             
             yield return new WaitForSeconds(1f); 
            EndTurn();
        }

        private IEnumerator TurnTimer()
        {
            while (_turnTimer > 0f && !_activePlayer.ShouldSkipTurn())
            {
                _turnTimer -= Time.deltaTime;
                _nextTurnButton.UpdateTurnTimer(_turnTimer);

                yield return null;
            }
        }

        public void EndTurn()
        {
            _activePlayer.EndTurn();

            if (_activePlayer == HumanPlayer)
            {
                if (_playerTurnSkipped || _turnTimer <= 0f)
                    Debug.Log(_activePlayer.PlayerName + " skipped their turn.");
                _activePlayer = BotPlayer;
            }
            else
                _activePlayer = HumanPlayer;

            StartTurn();
        }
        
        private int GetEnemyCurrentMana() => BotPlayer.ManaObject.ManaCurrent;
    }
}
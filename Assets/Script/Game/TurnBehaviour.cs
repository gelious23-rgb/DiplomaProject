using Script.Card.CardEffects;
using System.Collections;
using System.Collections.Generic;
using Script.Card;
using Script.Card.CardEffects;
using Script.Characters.Enemy;
using Script.Characters.Player;
using Script.Services;
using Script.Spawner;
using Script.UI;
using Script.UI.Buttons;
using Unity.Netcode;
using UnityEngine;

namespace Script.Game
{
    public class TurnBehaviour : NetworkBehaviour
    {
        private NetworkVariable<int> _turn = new(0);
        private int _maxMana = 1;
        
        [Header("Player")]
        [SerializeField] private PlayerSpawnerCards PlayerSpawnerCards;
        [SerializeField] private PlayerMana _playerMana;
        
        [Header("Enemy")]
        [SerializeField] private EnemySpawnerCards EnemySpawnerCards;
        [SerializeField] private EnemyAI _enemyAI;
        [SerializeField] private EnemyMana _enemyMana;

        [SerializeField] private UiTimer TimerReset;
        [SerializeField] private EndTurnButton _endTurnButton;
        [SerializeField] private CalculateDamage _calculateDamage;
        private CardDeath CardDeath;
        public NetworkVariable<bool> IsPlayerTurn = new(true);

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsHost)
            {
                _turn.OnValueChanged += OnValueChanged;
                IsPlayerTurn.Value = _turn.Value % 2 == 0;
            }
            
            if(IsHost)
                _endTurnButton.EndTurn.interactable = IsPlayerTurn.Value;
            else
                _endTurnButton.EndTurn.interactable = !IsPlayerTurn.Value;
            
        }

        public void StartGame() => StartCoroutine(TurnFunc());

        private void OnValueChanged(int previousvalue, int newvalue)
        {
            IsPlayerTurn.Value = newvalue % 2 == 0;
            UpdateEndButtonClientRpc(IsPlayerTurn.Value);
        }

        [ClientRpc]
        private void UpdateEndButtonClientRpc(bool value)
        {
            Debug.Log($"Update End Button {IsHost} {value}");
            if(IsHost)
                _endTurnButton.EndTurn.interactable = value;
            else
                _endTurnButton.EndTurn.interactable = !value;
        }

        IEnumerator TurnFunc()
        {
            if (_turn.Value % 2 == 0)
            {
                //CardEffectHandler.OnTurnStart.Invoke();
            }
            TimerReset.ResetTime();
            
            PrepareTurnClientRpc();
            if (IsPlayerTurn.Value)
                HandlePlayerTurn();
            else
                HandleEnemyAITurn();
            
            foreach (var p in TimerReset.NextTurnTime())
                yield return p;
            ChangeTurnServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        public void ChangeTurnServerRpc()
        {
            StopAllCoroutines();
            TurnEndClientRpc();
            _turn.Value++;
            StartCoroutine(TurnFunc());
        }

        [ClientRpc]
        private void TurnEndClientRpc()
        {
            _calculateDamage.CheckAmountCardsForCalculateDamage();
            if (IsPlayerTurn.Value)
            {
                PlayerSpawnerCards.GiveNewCards();
                EnemySpawnerCards.GiveNewCards(); 
                UpdateMana();
                _playerMana.ShowMana();
                _enemyMana.ShowMana();
            }
        }

        private void HandlePlayerTurn()
        {
            foreach (var card in PlayerSpawnerCards.Board)
            {
                card.ChangeAttackState(true);
                card.HighlightCard();
            }
        }

        private void HandleEnemyAITurn()
        {
            foreach (var card in EnemySpawnerCards.Board)
                card.ChangeAttackState(true);
//          _enemyAI.MakeTurn();
        }

        [ClientRpc]
        private void PrepareTurnClientRpc()
        {
            foreach (var card in PlayerSpawnerCards.Board)
                card.DeHighlightCard();
            CheckCardsForAvailability();
        }

        private void UpdateMana()
        {
            _maxMana = Mathf.Min(_maxMana + 1, 10);
            _playerMana.CurrentPlayerMana = _enemyMana.CurrentEnemyMana = _maxMana;
        }

        public void CheckCardsForAvailability()
        {
            foreach (var card in PlayerSpawnerCards.PlayerHandCards)
                card.CheckForAvailability(_playerMana.CurrentPlayerMana);
        }
        public void HighlightTargets(bool highlight)
        {
            foreach (var card in EnemySpawnerCards.Board)
                card.HighlightAsTarget(highlight);
        }
    }
}
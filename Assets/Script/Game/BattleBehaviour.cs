using Script.Card;
using Script.Card.CardEffects;
using Script.Characters.Player;
using Script.Services;
using Script.Spawner;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
namespace Script.Game
{
    public class BattleBehaviour : NetworkBehaviour
    {
        [SerializeField] internal CardDeath CardDeath;
        [SerializeField] internal CalculateDamage _calculateDamage;
        [SerializeField] private PlayerSpawnerCards _playerSpawnerCards;
        [SerializeField] private EnemySpawnerCards _enemySpawnerCards;

        public void CardAttacking(int targetIndex, int attakerIndex)
        {
            Debug.Log($"{attakerIndex} ATTACk {targetIndex}");
            var attaker = _playerSpawnerCards.Board[attakerIndex];
            var target = _enemySpawnerCards.Board[targetIndex];
            target.TakeDamage(attaker.ATK, attaker);
            CheckAlivePlayerCardOnBoard(attaker);
            CheckAliveEnemyCardOnBoard(target);
        }

        [ServerRpc]
        public void CardAttackingServerRpc(int targetIndex,int attakerIndex)
        {
            Debug.Log($"{attakerIndex} ATTACk {targetIndex}");
            var attaker = _enemySpawnerCards.Board[attakerIndex];
            var target = _playerSpawnerCards.Board[targetIndex];
            target.TakeDamage(attaker.ATK, attaker);
            CheckAlivePlayerCardOnBoard(attaker);
            CheckAliveEnemyCardOnBoard(target);
        }

        [ClientRpc]
        public void CardAttackingClientRpc(int targetIndex, int attakerIndex)
        {
            if(IsHost) return;
            Debug.Log($"{attakerIndex} ATTACk {targetIndex}");
            var attaker = _enemySpawnerCards.Board[attakerIndex];
            var target = _playerSpawnerCards.Board[targetIndex];
            target.TakeDamage(attaker.ATK, attaker);
            CheckAlivePlayerCardOnBoard(attaker);
            CheckAliveEnemyCardOnBoard(target);
        }

        public void CheckAliveEnemyCardOnBoard(CardInfoDisplay enemyCard)
        {
            if (!enemyCard.IsAlive)
            {
                CardDeath.DestroyCard(enemyCard);
                int damageDealt = enemyCard.CharacterCard.manacost;
                _calculateDamage.DealDamageToEnemyHero(damageDealt);
            }
            else
                enemyCard.RefreshData();
        }
        public void CheckAlivePlayerCardOnBoard(CardInfoDisplay playerCard)
        {
            if (!playerCard.IsAlive)
            {
                CardDeath.DestroyCard(playerCard);
                int damageDealt = playerCard.CharacterCard.manacost;
                _calculateDamage.DealDamageToPlayerHero(damageDealt);
            }
            else
                playerCard.RefreshData();
        }


    }
}
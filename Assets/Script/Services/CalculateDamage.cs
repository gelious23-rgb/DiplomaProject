using Script.Characters.Enemy;
using Script.Characters.Player;
using Script.Game;
using Script.Spawner;

using UnityEngine;


namespace Script.Services
{
    public class CalculateDamage : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerDeath _playerDeath;
        [SerializeField] private PlayerSpawnerCards _playerSpawnerCards;

        [SerializeField] private EnemyDeath _enemyDeath;
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private EnemySpawnerCards EnemySpawnerCards;
        
        [SerializeField] private TurnBehaviour _turnBehaviour;


        private int CalculateDamageToEnemyForActiveCards() 
        { 
            int damageDealt = 0;
            
            foreach (var activeCard in _playerSpawnerCards.PlayerFieldCards.FindAll(x => x.CanAttack))
                damageDealt += activeCard.CharacterCard.manacost;
            return damageDealt; 
        }
        private int CalculateDamageToPlayerForActiveCards() 
        {
            int damageDealt = 0;

            foreach (var activeCard in EnemySpawnerCards.EnemyFieldCards.FindAll(x => x.CanAttack))
                damageDealt += activeCard.CharacterCard.manacost;
            return damageDealt; 
        } 
        
        public void DealDamageToEnemyHero(int damage)
        {

            int currentEnemyHealth = _enemyHealth.CurrentHealth;
            _enemyHealth.TakeDamage(damage);
            _enemyHealth.Show();
            _enemyDeath.Death(ref currentEnemyHealth);

        }
        
        public void DealDamageToPlayerHero(int damage)
        {
            int currentPlayerHealth = _playerHealth.CurrentHealth;
            _playerHealth.TakeDamage(damage);
            _playerHealth.Show();
            _playerDeath.Death( currentPlayerHealth);
        }

        public void CheckAmountCardsForCalculateDamage()
        {
            if (_turnBehaviour.IsPlayerTurn)
                CalculateDamageForPlayerTurn();
            else
                CalculateDamageForEnemyTurn();
        }

        private void CalculateDamageForPlayerTurn()
        {
            if (_playerSpawnerCards.PlayerFieldCards.Exists(x => x.CanAttack) && EnemySpawnerCards.EnemyFieldCards.Count == 0)
            {
                int damageDealt = CalculateDamageToEnemyForActiveCards();
                if (damageDealt > 0)
                    DealDamageToEnemyHero(damageDealt);
            }
        }

        private void CalculateDamageForEnemyTurn()
        {
            if (EnemySpawnerCards.EnemyFieldCards.Exists(x => x.CanAttack) && _playerSpawnerCards.PlayerFieldCards.Count == 0)
            {
                int damageDealt = CalculateDamageToPlayerForActiveCards();
                if (damageDealt > 0)
                    DealDamageToPlayerHero(damageDealt);
            }
        }
    }
}
using Script.Card;
using Script.Services;
using UnityEngine;
namespace Script.Game
{
    public class BattleBehaviour : MonoBehaviour
    {
        [SerializeField] private TurnBehaviour TurnBehaviour;
        
        [SerializeField] private CardDeath CardDeath;
        
        [SerializeField] private CalculateDamage _calculateDamage;

        public void CardsFight(CardInfoDisplay playerCard, CardInfoDisplay enemyCard)
        {
            if (TurnBehaviour.IsPlayerTurn)
                enemyCard.CharacterCard.GetDamage(playerCard.CharacterCard.Damage);
            else
                playerCard.CharacterCard.GetDamage(enemyCard.CharacterCard.Damage);
            
            CheckAlivePlayerCardOnBoard(playerCard);
            CheckAliveEnemyCardOnBoard(enemyCard);
        }
        private void CheckAliveEnemyCardOnBoard(CardInfoDisplay enemyCard)
        {
            if (!enemyCard.CharacterCard.IsAlive)
            {
                CardDeath.DestroyCard(enemyCard);
                int damageDealt = enemyCard.CharacterCard.Manacost;
                _calculateDamage.DealDamageToEnemyHero(damageDealt);
            }
            else
                enemyCard.RefreshData();
        }
        private void CheckAlivePlayerCardOnBoard(CardInfoDisplay playerCard)
        {
            if (!playerCard.CharacterCard.IsAlive)
            {
                CardDeath.DestroyCard(playerCard);
                int damageDealt = playerCard.CharacterCard.Manacost;
                _calculateDamage.DealDamageToPlayerHero(damageDealt);
            }
            else
                playerCard.RefreshData();
        }
    }
}
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
                enemyCard.GetDamage(playerCard.ATK);
            else
                playerCard.GetDamage(enemyCard.ATK);
            
            CheckAlivePlayerCardOnBoard(playerCard);
            CheckAliveEnemyCardOnBoard(enemyCard);
        }
        private void CheckAliveEnemyCardOnBoard(CardInfoDisplay enemyCard)
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
        private void CheckAlivePlayerCardOnBoard(CardInfoDisplay playerCard)
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
        public void CardsForceFight(CardInfoDisplay Card, CardInfoDisplay target) => Card.GetDamage(target.ATK);
    }
}
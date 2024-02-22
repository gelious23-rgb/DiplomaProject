using Script.Card;
using Script.Card.CardEffects;
using Script.Services;
using Unity.VisualScripting;
using UnityEngine;
namespace Script.Game
{
    public class BattleBehaviour : MonoBehaviour
    {
        [SerializeField] private TurnBehaviour TurnBehaviour;
        
        [SerializeField] private CardDeath CardDeath;
        
        [SerializeField] private CalculateDamage _calculateDamage;

       /* public void CardsFight(CardInfoDisplay playerCard, CardInfoDisplay enemyCard)
        {
            if (TurnBehaviour.IsPlayerTurn)
            {
                //CardEffectHandler.OnBeingHit.Invoke(enemyCard);
                enemyCard.TakeDamage(playerCard.ATK, playerCard);
            }
            else
            {
               // CardEffectHandler.OnBeingHit.Invoke(playerCard);
                playerCard.TakeDamage(enemyCard.ATK, enemyCard);
            }
            
            CheckAlivePlayerCardOnBoard(playerCard);
            CheckAliveEnemyCardOnBoard(enemyCard);
        }*/

        public void CardAttacking(CardInfoDisplay target,CardInfoDisplay attaker)
        { 
            foreach (var effectClass in attaker.GetComponents<Effect>())
            {
                CardEffectHandler.OnAttack.AddListener(effectClass.OnAttack);
            }
            foreach (var effectClass in target.GetComponents<Effect>())
            {
                CardEffectHandler.OnBeingHit.AddListener(effectClass.OnBeingHit);
            }
            CardEffectHandler.OnBeingHit.Invoke(target, attaker);
            CardEffectHandler.OnAttack.Invoke(attaker, target);
            Debug.Log(target.CharacterCard.name + "is being hit by " + attaker.CharacterCard.name);
            target.TakeDamage(attaker.ATK, attaker);
            Debug.Log(attaker.CharacterCard.name + "deals " + attaker.ATK + " damage to " + target.CharacterCard.name);
            CheckAlivePlayerCardOnBoard(target);
            CheckAliveEnemyCardOnBoard(attaker);
            foreach (var effectClass in attaker.GetComponents<Effect>())
            {
                CardEffectHandler.OnAttack.RemoveListener(effectClass.OnAttack);
            }
            foreach (var effectClass in target.GetComponents<Effect>())
            {
                CardEffectHandler.OnBeingHit.RemoveListener(effectClass.OnBeingHit);
            }
        }

        /*public void CardsForceFight(CardInfoDisplay Card, CardInfoDisplay target)
        { 
           // CardEffectHandler.OnBeingHit.Invoke(Card, target);
            Card.TakeDamage(target.ATK, target);
        }*/
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
    }
}
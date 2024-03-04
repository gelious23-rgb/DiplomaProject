using UnityEngine;

namespace Script.Card.CardEffects
{
    public class CounterAttack : Effect
    {
        public override void OnBeingHitAfter(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            BattleBehaviour.CheckAliveEnemyCardOnBoard(target);
            BattleBehaviour.CheckAlivePlayerCardOnBoard(target);
            if (GetCard().CurrentHP>0 && target == GetCard())
            {
                Debug.Log(target.CharacterCard.name + " is attacked by " +
                          damageSource.CharacterCard.name + " " + target.CharacterCard.name + " counterattacks for "
                          + target.ATK + " damage");
                damageSource.TakeDamage(target.ATK, damageSource);
            }

        }
        
    }
}

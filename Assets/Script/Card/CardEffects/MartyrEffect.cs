using System;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class MartyrEffect : Effect
    {
        private CardInfoDisplay attacker;
        public override void DoOnEnable()
        {
            CardEffectHandler.OnDeath.AddListener(OnDeath);
        }

        public void OnDeath(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                Debug.Log("Martyr effect 1");
                if (GetCard().HP <= 0)
                {
                    Debug.Log("Martyr effect 2");
                    attacker.TakeDamage(1, GetCard());
                }
            }
        }
        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            attacker = damageSource;
        }
        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
        }

        
    }
}

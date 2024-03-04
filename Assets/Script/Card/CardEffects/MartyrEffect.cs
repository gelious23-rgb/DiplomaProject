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

        private void OnDeath(CardInfoDisplay self)
        {
          
            if (self == GetCard() && GetCard().IsPlaced)
            {
                Debug.Log(self.CharacterCard.name + "died");
                if (GetCard().CurrentHP <= 0)
                {
                    Debug.Log("Martyr effect 2");
                    if (attacker != null && attacker.IsAlive)
                    {
                        attacker.TakeDamage(1, GetCard());
                    }
                }
                CardEffectHandler.OnDeath.RemoveListener(OnDeath);
            }
            
        }
        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            attacker = damageSource;
            if (attacker.IsAlive == false)
            {
                attacker = null;
            }
        }
        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
        }

        
    }
}

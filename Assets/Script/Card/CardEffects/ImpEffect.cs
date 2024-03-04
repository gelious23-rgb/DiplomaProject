using System;
using Unity.VisualScripting;

namespace Script.Card.CardEffects
{
    public class ImpEffect : Effect
    {
       
        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            Blessing debuff =target.AddComponent<Blessing>();
             
            debuff.ATKBlessing = -1;
            debuff.destroyOnTurnEnd = true;
             
            debuff.ApplyBlessings();
        }

    }
}

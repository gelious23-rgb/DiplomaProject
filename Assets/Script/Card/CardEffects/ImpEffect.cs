using System;
using Unity.VisualScripting;

namespace Script.Card.CardEffects
{
    public class ImpEffect : Effect
    {
       
        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            /*Blessing debuff =target.AddComponent<Blessing>();
             
            debuff.ATKBlessing = -1;
            debuff.destrotOnTurnEnd = true;
             
            debuff.ApplyBlessings();*/
            Blessing debugBuff = target.AddComponent<Blessing>();
             
            debugBuff.HpBlessing = 3;
            debugBuff.destrotOnTurnEnd = true;
            
            debugBuff.ApplyBlessings();
            GetCard().RefreshData();
        }

    }
}

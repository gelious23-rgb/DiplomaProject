using UnityEngine;

namespace Script.Card.CardEffects
{
    public class HolyLanceEffect : Effect
    {
        public override void OnAttack(CardInfoDisplay target, CardInfoDisplay self)
        {
            BattleBehaviour.CardDeath.DestroyCard(target);
            BattleBehaviour.CardDeath.DestroyCard(self);
        }
        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
        }
    }
}

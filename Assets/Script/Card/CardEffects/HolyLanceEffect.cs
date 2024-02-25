using UnityEngine;

namespace Script.Card.CardEffects
{
    public class HolyLanceEffect : Effect
    {
        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
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

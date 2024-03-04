using UnityEngine;

namespace Script.Card.CardEffects
{
    public class HolyLanceEffect : Effect
    {
        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            BattleBehaviour.CardDeath.DestroyCard(target);
            Destroy(this);
          
        }
        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
        }
    }
}

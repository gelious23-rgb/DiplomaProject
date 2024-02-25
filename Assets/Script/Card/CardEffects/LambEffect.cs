using UnityEngine;

namespace Script.Card.CardEffects
{
    public class LambEffect : Effect
    {
        private int CardTurnsAlive =0;
        protected override void OnTurnEnd()
        {
            CardTurnsAlive++;
            if (CardTurnsAlive > 3)
            {
                BattleBehaviour._calculateDamage.DealDamageToEnemyHero(GetCard().HP);
                BattleBehaviour._calculateDamage.DealDamageToPlayerHero(GetCard().HP);
            }
        }
    }
}

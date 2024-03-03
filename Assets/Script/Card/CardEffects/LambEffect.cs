using UnityEngine;

namespace Script.Card.CardEffects
{
    public class LambEffect : Effect
    {
        private int CardTurnsAlive =0;
        protected override void OnTurnEnd()
        {
            if (GetCard().IsPlaced)
            {
                CardTurnsAlive++;
                if (CardTurnsAlive > 3)
                {
                    BattleBehaviour._calculateDamage.DealDamageToEnemyHero(GetCard().CharacterCard.manacost);
                    BattleBehaviour._calculateDamage.DealDamageToPlayerHero(GetCard().CharacterCard.manacost);
                }
            }
           
        }
    }
}

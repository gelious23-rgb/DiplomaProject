using UnityEngine;

namespace Script.Card.CardEffects
{
    public class WarlockEffect : Effect
    {
        public override void DoOnEnable()
        {
            CardEffectHandler.OnBeingPlayed.AddListener(OnAllyBeingPlayed);
        }

        private void OnAllyBeingPlayed(CardInfoDisplay ally)
        {
            if (GetCard().IsPlaced && ally!= GetCard())
            {
                if (ally.owner == GetCard().owner)
                {
                    ally.gameObject.AddComponent<Wrath>();
                    Destroy(this);
                }
            }
        }
    }
}

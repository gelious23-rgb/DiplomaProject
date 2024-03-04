using Unity.VisualScripting;

namespace Script.Card.CardEffects
{
    public class Wrath : Effect
    {
        private bool ApplyWrath = false;
        public override void DoOnEnable()
        {
            destroyOnTurnEnd = true;
        }

        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            if (ApplyWrath == false)
            {
                ApplyWrath = true;
            }
        }

        protected override void OnTurnEnd()
        {
            if (ApplyWrath)
            {
                Blessing wrathBuff = GetCard().AddComponent<Blessing>();
                wrathBuff.ATKBlessing = GetCard().ATK;
                wrathBuff.ApplyBlessings();
                wrathBuff.destroyOnTurnEnd = true;
            }
            base.OnTurnEnd();
        }
    }
}

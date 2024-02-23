using UnityEngine;
using UnityEngine.UI;

namespace Script.Card.CardEffects
{
    public class Protection : Effect
    {
        public float multiplier = 0.5f;
        protected override void OnTurnStart()
        {
            GetCard().gameObject.GetComponent<Outline>().enabled = true;
        }

        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            if (target == GetCard())
            {
                var value = Mathf.RoundToInt(target.ATK * multiplier);
                target.DamageResistance = value;
                Debug.Log("protection passive active power is " + value);
            }
        }

        private void OnDestroy()
        {
            GetCard().gameObject.GetComponent<Outline>().enabled = false;
            GetCard().DamageResistance = 0;
        }

        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
        }
    }
}

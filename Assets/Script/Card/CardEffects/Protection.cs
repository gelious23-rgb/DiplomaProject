using UnityEngine;
using UnityEngine.UI;

namespace Script.Card.CardEffects
{
    public class Protection : Effect
    {
        protected override void OnTurnStart()
        {
            GetCard().gameObject.GetComponent<Outline>().enabled = true;
        }

        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            if (target == GetCard())
            {
                var value = Mathf.RoundToInt(target.ATK * 0.5f);
                target.DamageResistance = value;
                Debug.Log("protection passive active power is " + value);
            }
        }

        private void OnDestroy()
        {
            GetCard().gameObject.GetComponent<Outline>().enabled = false;
            GetCard().DamageResistance = 0;
        }
    }
}

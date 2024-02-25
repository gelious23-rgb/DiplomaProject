using UnityEngine;
using UnityEngine.UI;

namespace Script.Card.CardEffects
{
    public class Protection : Effect
    {
        public float multiplier = 0.5f;
        public override void DoOnEnable()
        {
            GetCard().gameObject.GetComponent<Outline>().effectColor = Color.cyan;
        }

        protected override void OnTurnStart()
        {
           
        }

        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            Debug.Log(target.CharacterCard.name +" is a target " +
                      damageSource.CharacterCard.name + " is an attacker");
            if (target == GetCard())
            {
                var value = Mathf.RoundToInt(target.ATK * multiplier);
                target.DamageResistance = value;
                Debug.Log("protection passive active power is " + value);
            }
        }

        private void OnDestroy()
        {
            GetCard().gameObject.GetComponent<Outline>().effectColor = Color.clear;
            GetCard().DamageResistance = 0;
        }

        protected override void OnTurnEnd()
        {
            //base.OnTurnEnd();
        }
    }
}

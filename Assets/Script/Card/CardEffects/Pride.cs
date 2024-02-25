using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card.CardEffects
{
    public class Pride : Effect
    {
        private Protection _protection;
        public override void DoOnEnable()
        {
            StartCoroutine(Delay(0.1f));
            _protection = GetComponent<Protection>();
            _protection.multiplier = 1f;
            GetCard().gameObject.GetComponent<Outline>().effectColor = Color.yellow;
        }

        private IEnumerator Delay(float t)
        {
            yield return new WaitForSeconds(t);
        }
        
    }
}

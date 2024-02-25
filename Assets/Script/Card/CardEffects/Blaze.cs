using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Blaze : Effect
    {
        
        public override void DoOnEnable()
        {
            
        }

        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            if (self == GetCard())
            {
                if (target.gameObject.TryGetComponent<Burn>(out Burn br) == false)
                {
                    Debug.Log("Target has no burn");
                    var burn = target.AddComponent<Burn>();
                    burn.burnPower = 1;
                }
                else
                {
                    Debug.Log("Target has "+br.burnPower + "Burn");
                    br.burnPower++; 
                    br.Count.text = br.burnPower.ToString();
                }
            }
        }

        public override void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            if (damageSource.gameObject.TryGetComponent<Burn>(out Burn br) == false)
            {
                Debug.Log("Attacker has no burn");
                var burn = damageSource.AddComponent<Burn>();
                burn.burnPower = 1;
            }
            else
            {
                Debug.Log("Attacker has "+br.burnPower + "Burn");
                br.burnPower++;
                br.Count.text = br.burnPower.ToString();
            }
        }

        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
        }
    }
}

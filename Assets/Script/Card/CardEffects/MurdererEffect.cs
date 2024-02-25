using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class MurdererEffect : Effect
    {
        public override void DoOnEnable()
        {
            CardEffectHandler.OnDeath.AddListener(MurdererIncreaseATK);
        }

        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            if (self == GetCard())
            {
                if (target.gameObject.TryGetComponent<Bleed>(out var br) == false)
                {
                    Debug.Log("Target has no Bleed");
                    var burn = target.AddComponent<Bleed>();
                    burn.bleedPower = 2;
                }
                else
                {
                    Debug.Log("Target has "+br.bleedPower + "Bleed");
                    br.bleedPower+=2; 
                    br.Count.text = br.bleedPower.ToString();
                }
            }
        }

        private void MurdererIncreaseATK(CardInfoDisplay cardThatDied)
        {
            GetCard().ATK++;
            GetCard().RefreshData();
        }
    }
}

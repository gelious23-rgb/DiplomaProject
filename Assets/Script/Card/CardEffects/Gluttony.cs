using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Gluttony : Effect
    {
        private Blessing Glutton;
        public override void DoOnEnable()
        {
            Blessing GluttonyBless = GetCard().gameObject.AddComponent<Blessing>();
            Glutton = GluttonyBless;
            Glutton.HpBlessing = 0;
            Glutton.ATKBlessing = 0;
            Glutton.ApplyBlessings();
        }

        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            if (self == GetCard())
            {
                var Glvalue = GetCard().ATK * 0.5f;
               Glutton.ATKBlessing += self.Heal(Mathf.RoundToInt(Glvalue)); 
            }
        }
    }
}

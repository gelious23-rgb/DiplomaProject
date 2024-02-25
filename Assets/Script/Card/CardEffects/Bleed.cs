using TMPro;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Bleed : Effect
    {
        public int bleedPower =2;
        
        public TextMeshProUGUI Count;
        private GameObject BuffIcon;
        public override void DoOnEnable()
        {
            BuffIcon = GetCard().BuffSpriteSpace.transform.GetChild(1).gameObject;
            BuffIcon.SetActive(true);
            Count = BuffIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Count.text = bleedPower.ToString();
        }

        public override void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            GetCard().HP -= bleedPower;
            GetCard().RefreshData();
            if (GetCard().HP == 0)
            { 
                BattleBehaviour.CheckAliveEnemyCardOnBoard(GetCard());
               BattleBehaviour.CheckAliveEnemyCardOnBoard(GetCard());
            }
            if (bleedPower > 0)
            {
                // ReSharper disable once PossibleLossOfFraction
                bleedPower = Mathf.RoundToInt(bleedPower/2);
                Count.text = bleedPower.ToString();
            }

            if (bleedPower == 0)
            {
                Destroy(this);
                BuffIcon.SetActive(false);
            }
        }
    }
    
}

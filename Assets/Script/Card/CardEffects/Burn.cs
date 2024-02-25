using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card.CardEffects
{
    public class Burn : Effect
    {
        public int burnPower =1;
        
        public TextMeshProUGUI Count;
        private GameObject BuffIcon;
        public override void DoOnEnable()
        {
            BuffIcon = GetCard().BuffSpriteSpace.transform.GetChild(0).gameObject;
            BuffIcon.SetActive(true);
            Count = BuffIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Count.text = burnPower.ToString();
        }

        protected override void OnTurnEnd()
        {
             GetCard().HP-=burnPower;
             GetCard().RefreshData();
             if (GetCard().HP == 0)
             { 
                 BattleBehaviour.CheckAliveEnemyCardOnBoard(GetCard());
                 BattleBehaviour.CheckAliveEnemyCardOnBoard(GetCard());
             }
             if (burnPower > 0 && burnPower != 1)
             {
                 // ReSharper disable once PossibleLossOfFraction
                 burnPower = Mathf.RoundToInt(burnPower/2);
                 Count.text = burnPower.ToString();
             }

             if (burnPower is 1 or 0)
             {
                 Destroy(this);
                 BuffIcon.SetActive(false);
             }
        }
        
    }
}

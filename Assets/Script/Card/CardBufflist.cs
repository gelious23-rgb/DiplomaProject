using System;
using System.Collections.Generic;
using Script.Card.CardEffects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card
{
    public class CardBufflist : MonoBehaviour
    {
        [SerializeField] internal CardInfoDisplay thisCard;
        [SerializeField] internal GameObject AtkSprite, HpSprite, BurnSprite, TransmutationSprite, BleedSprite,
            MiracleSprite, ElixirSprite;
        [SerializeField] internal TextMeshProUGUI AtkBlessTextDisplay, HpBlessTextDisplay, BurnTextDisplay, BleedTextDisplay;

        private void OnEnable()
        {
            CardEffectHandler.OnBeingHit.AddListener(Bufflist);
            CardEffectHandler.OnAttack.AddListener(Bufflist);
            CardEffectHandler.OnTurnEnd.AddListener(CallBufflist);
            CardEffectHandler.OnTurnStart.AddListener(CallBufflist);
            CardEffectHandler.OnBeingPlayed.AddListener(callBufflist);
            
        }

        public void CallBufflist()
        {
            Bufflist(null, null);
        }

        private void callBufflist(CardInfoDisplay cardPlcHldr)
        {
            Bufflist(null, null);
        }

        private void Bufflist(CardInfoDisplay card1, CardInfoDisplay card2)
        {
            var allBlessings = thisCard.gameObject.GetComponents<Blessing>();
            if (allBlessings != null)
            {
                int totalAttack = 0;
                int totalHp = 0;
                foreach (var blessing in allBlessings)
                {
                    totalHp += blessing.HpBlessing;
                    totalAttack += blessing.ATKBlessing;
                }

                if (totalHp != 0)
                {
                    HpSprite.SetActive(true);
                    HpBlessTextDisplay.text = totalHp.ToString();
                }

                if (totalHp == 0)
                {
                    HpSprite.SetActive(false);
                }
                if (totalAttack != 0)
                {
                    AtkSprite.SetActive(true);
                    AtkBlessTextDisplay.text = totalAttack.ToString();
                }

                if (totalAttack == 0)
                {
                    AtkSprite.SetActive(false);
                }
                
            }
        }

        
        
    }
}

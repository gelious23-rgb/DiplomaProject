using System;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Blessing : Effect
    {
        public int HpBlessing=0;
        public int ATKBlessing = 0;

        public override void DoOnEnable()
        {
            if (GetCard().CharacterCard.name == "Star of the Morning")
            {
                ATKBlessing = 2;
            }
            if (GetCard().CharacterCard.name == "Crux")
            {
                HpBlessing = 2;
            }
            
            ApplyBlessings();
        }

        protected internal void ApplyBlessings()
        {
            GetCard().MaxHp += HpBlessing;
            GetCard().Heal(HpBlessing);
            GetCard().ATK += ATKBlessing;
            GetCard().RefreshData();
            GetCard().Bufflist.CallBufflist();
        }

        private protected void CheckAmount()
        {
             
            if (GetCard().ATK < 0)
            {
                GetCard().ATK = 0;
            }
            if (GetCard().CurrentHP <= 0 ||GetCard().MaxHp <= 0)
            {
                BattleBehaviour.CardDeath.DestroyCard(GetCard());
            }
        }

        protected override void OnTurnEnd()
        {
            if (destroyOnTurnEnd)
            {
                if (HpBlessing > 0)
                {
                    GetCard().MaxHp -= HpBlessing;
                    //  GetCard().CurrentHP += HpBlessing;
                }
                else if(HpBlessing <0)
                {
                    GetCard().MaxHp +=-HpBlessing;
                }
                if (ATKBlessing > 0)
                {
                    GetCard().ATK -= ATKBlessing;
                    //  GetCard().CurrentHP += HpBlessing;
                }
                else if(ATKBlessing <0)
                {
                    GetCard().ATK +=-ATKBlessing;
                }
                GetCard().Bufflist.CallBufflist();
            }
            
            GetCard().RefreshData();
          
            base.OnTurnEnd();
        }
    }
    
    
}

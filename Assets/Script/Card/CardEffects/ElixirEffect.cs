using System;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class ElixirEffect : Effect
    {
        public override void DoOnEnable()
        {
            destrotOnTurnEnd = true;
            GetCard().Bufflist.ElixirSprite.SetActive(true);
        }

        protected override void OnTurnEnd()
        {
            GetCard().Heal(GetCard().MaxHp);
        }

        private void OnDestroy()
        {
            GetCard().Bufflist.ElixirSprite.SetActive(false);
        }
    }
}

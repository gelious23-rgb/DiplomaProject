using System;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Miracle : Effect
    {
        public override void DoOnEnable()
        {
            destrotOnTurnEnd = true;
            GetCard().Bufflist.MiracleSprite.SetActive(true);
        }

        private void OnDestroy()
        {
             GetCard().Bufflist.MiracleSprite.SetActive(false);
        }
    }
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class EdenFruitEffect : Effect
    {
        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                ApplyMiracle();
            }
        }

        private int RNG()
        {
            return Random.Range(0, GetCard().owner.Board.Count);
        }

        private void ApplyMiracle()
        {
            List<CardInfoDisplay> cardInfoDisplays = new List<CardInfoDisplay>();
            foreach (var cardInfoDisplay in GetCard().owner.Board)
            {
                if (cardInfoDisplay.CharacterCard.name != "Sisyphus")
                {
                    cardInfoDisplays.Add(cardInfoDisplay);
                }
            } 
            var target = cardInfoDisplays[RNG()];
            target.AddComponent<Miracle>();
        }

        protected override void OnTurnStart()
        {
            ApplyMiracle();
        }
    }
}

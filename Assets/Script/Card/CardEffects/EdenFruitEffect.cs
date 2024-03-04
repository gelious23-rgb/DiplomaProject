using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class EdenFruitEffect : Effect
    {
        // Declare cardInfoDisplays at the class level
        private List<CardInfoDisplay> cardInfoDisplays = new List<CardInfoDisplay>();

        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                ApplyMiracle();
            }
        }

        private int RNG()
        {
            return Random.Range(0, GetCard().owner.Board.Count - cardInfoDisplays.Count);
        }

        private void ApplyMiracle()
        {
            // Clear the list before using it
            cardInfoDisplays.Clear();

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
            if (GetCard().IsPlaced)
            {
                ApplyMiracle();
            }

        }
    }
}

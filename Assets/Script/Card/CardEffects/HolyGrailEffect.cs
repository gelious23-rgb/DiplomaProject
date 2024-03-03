using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class HolyGrailEffect : Effect
    {
        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            GetCard().AddComponent<WineEffect>();
            AddElixirToAll();
        }

        protected override void OnTurnStart()
        {
            AddElixirToAll();
        }

        private void AddElixirToAll()
        {
            foreach (var card in GetCard().owner.Board)
            {
                card.AddComponent<ElixirEffect>();
            }
        }
    }
}

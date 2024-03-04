using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class HolyGrailEffect : Effect
    {

        protected override void OnTurnEnd()
        {
            if (GetCard().IsPlaced)
            {
                AddElixirToAll();
            }

        }

        private void AddElixirToAll()
        {
            foreach (var card in GetCard().owner.Board)
            {
                /*card.AddComponent<ElixirEffect>();*/
                if (card != GetCard())
                {
                    ElixirEffect elixir = card.AddComponent<ElixirEffect>();
                    elixir.destroyOnTurnEnd = true;
                }
            }
        }
    }
}

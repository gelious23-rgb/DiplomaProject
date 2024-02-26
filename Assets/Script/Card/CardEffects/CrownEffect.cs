using System.Collections.Generic;
using Script.Spawner;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class CrownEffect : Effect
    {
        public List<CardInfoDisplay> board = new List<CardInfoDisplay>();
        
        protected override void OnTurnStart()
        {
           CardEffectHandler.OnBeingHit.AddListener(OnAllyHit);
           DeckUpdate();
        }

        public override void DoOnEnable()
        {
            CardEffectHandler.OnDeath.AddListener(DrawCard);
            CardEffectHandler.OnBeingPlayed.AddListener(OnAllyBeingPlayed);
            DeckUpdate();
        }

        private void DeckUpdate()
        {
            board.Clear();
            foreach (var card in GetCard().GetComponent<SpawnerCards>().Board)
            {
                board.Add(card);
            }
        }

        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                DeckUpdate();
             //    board = GetCard().owner.Board;
                
                foreach (var card in board)
                {
                    Blessing CrownHpBless = card.AddComponent<Blessing>();
                    CrownHpBless.HpBlessing = 2;
                    CrownHpBless.ApplyBlessings();
                }
            }
        }

        private void OnAllyBeingPlayed(CardInfoDisplay ally)
        {
            if (ally.owner == GetCard().owner)
            {
                Blessing CrownHpBless = ally.AddComponent<Blessing>();
                CrownHpBless.HpBlessing = 2;
                CrownHpBless.ApplyBlessings();
            }
        }

        private void OnAllyHit(CardInfoDisplay ally, CardInfoDisplay damageSource)
        {
            if (ally.owner.Board.Contains(GetCard()))
            {
                Blessing crownAtkBless = ally.AddComponent<Blessing>();
                crownAtkBless.ATKBlessing = 1;
                crownAtkBless.HpBlessing = 1;
                crownAtkBless.ApplyBlessings();
            }
        }

        private void DrawCard(CardInfoDisplay deadCard)
        {
            if (GetCard().IsPlaced)
            {
                if (deadCard.owner.IsPlayer)
                {
                    CardEffectHandler.GetLibrary().GiveCardToPlayerHand(Random.Range(0, 
                        CardEffectHandler.GetLibrary().AllCards.Count));
                }

                if (deadCard.owner.IsPlayer == false)
                {
                    CardEffectHandler.GetLibrary().GiveCardToEnemyHand(Random.Range(0, 
                        CardEffectHandler.GetLibrary().AllCards.Count));
                }   
            }
        }
    }
}

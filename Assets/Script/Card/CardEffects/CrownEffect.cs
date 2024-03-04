using System.Collections.Generic;
using Script.Spawner;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class CrownEffect : Effect
    {
       // public List<CardInfoDisplay> board = new List<CardInfoDisplay>();
        
        protected override void OnTurnStart()
        {
           CardEffectHandler.OnBeingHit.AddListener(OnAllyHit);
          if(GetCard().IsPlaced){ DeckUpdate();}
        }

        protected override void OnTurnEnd()
        {
            CardEffectHandler.OnBeingHit.RemoveListener(OnAllyHit);
        }

        public override void DoOnEnable()
        {
            CardEffectHandler.OnDeath.AddListener(DrawCard);
            CardEffectHandler.OnBeingPlayed.AddListener(OnAllyBeingPlayed);
             
            //DeckUpdate();
        }

        private List<CardInfoDisplay> DeckUpdate()
        {
            var cardd = GetCard();
            var spawner = cardd.owner.gameObject.GetComponent<SpawnerCards>();
            var spawnerBoard = spawner.Board;
            List<CardInfoDisplay> returnboard = new List<CardInfoDisplay>();
            returnboard  = spawnerBoard;
            //board.Clear();
            

            return returnboard;
            
        }

        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                //DeckUpdate();
             //    board = GetCard().owner.Board;
                
                foreach (var card in DeckUpdate())
                {
                    if (card != GetCard())
                    {
                        Blessing CrownHpBless = card.AddComponent<Blessing>();
                        CrownHpBless.HpBlessing = 2;
                        CrownHpBless.ApplyBlessings();
                    }
                }
            }
        }

        private void OnAllyBeingPlayed(CardInfoDisplay ally)
        {
            if (GetCard().IsPlaced && ally!= GetCard())
            {
                if (ally.owner == GetCard().owner)
                {
                    Blessing CrownHpBless = ally.AddComponent<Blessing>();
                    CrownHpBless.HpBlessing = 2;
                    CrownHpBless.ApplyBlessings();
                }
            }
        }

        private void OnAllyHit(CardInfoDisplay ally, CardInfoDisplay damageSource)
        {
           
            if (GetCard().IsPlaced)
            {
                Debug.Log("Crown atatck blessing activated " + "Target is "+ally.CharacterCard.name + "Offender is " + damageSource.CharacterCard.name);
                if (ally.owner.Board.Contains(GetCard()) && ally!= GetCard())
                {
                    Blessing crownAtkBless = ally.AddComponent<Blessing>();
                    crownAtkBless.ATKBlessing = 1;
                    crownAtkBless.HpBlessing = 1;
                    crownAtkBless.ApplyBlessings();
                    Bleed crownBleed = ally.AddComponent<Bleed>();
                    crownBleed.bleedPower = 1;
                    crownBleed.destroyOnTurnEnd = true;
                }
            }
        }

        private void DrawCard(CardInfoDisplay deadCard)
        {
            if (GetCard().IsPlaced)
            {
                if (deadCard.owner.IsPlayer && GetCard().owner == deadCard.owner)
                {
                    CardEffectHandler.GetLibrary().GiveCardToPlayerHand(Random.Range(0, 
                        CardEffectHandler.GetLibrary().AllCards.Count));
                }

                if (deadCard.owner.IsPlayer == false && GetCard().owner == deadCard.owner)
                {
                    CardEffectHandler.GetLibrary().GiveCardToEnemyHand(Random.Range(0, 
                        CardEffectHandler.GetLibrary().AllCards.Count));
                }   
            }
        }
    }
}

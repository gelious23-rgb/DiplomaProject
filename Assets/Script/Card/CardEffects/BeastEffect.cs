using System;
using Script.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Card.CardEffects
{
    public class BeastEffect : Effect
    {
        private PlayerSpawnerCards playerCards;
        private EnemySpawnerCards enemyCards;
        private int CardTurnsAlive = 0;
        private int BeastTurnsStarved = 0;
        public override void DoOnEnable()
        {
            playerCards = FindObjectOfType<PlayerSpawnerCards>();
            enemyCards = FindObjectOfType<EnemySpawnerCards>();
        }

        protected override void OnTurnEnd()
        {
            if (GetCard().IsPlaced)
            {
                CardTurnsAlive++;

                if (CardTurnsAlive > 3)
                {
                    CardInfoDisplay Sacrifice = null;
                    if (enemyCards.Board.Contains(GetCard()))
                    {
                        Sacrifice = playerCards.Board[Random.Range(0, playerCards.Board.Count + 1)];
                        BattleBehaviour.CardDeath.DestroyCard(Sacrifice);
                        BeastTurnsStarved = 0;
                    }
                    else if (playerCards.Board.Contains(GetCard()))
                    {
                        Sacrifice =enemyCards.Board[Random.Range(0, enemyCards.Board.Count + 1)];
                        BattleBehaviour.CardDeath.DestroyCard(Sacrifice);
                        BeastTurnsStarved = 0;
                    }
                    else if (Sacrifice == null)
                    {
                        Debug.Log("Nothing for the beast to destroy");
                        if (BeastTurnsStarved > 1)
                        {
                            BattleBehaviour.CardDeath.DestroyCard(GetCard());
                        }
                        else
                        {
                            BattleBehaviour._calculateDamage.DealDamageToCharacterDirectly(GetCard().owner,GetCard().CharacterCard.manacost);
                            BeastTurnsStarved++;
                        }
                    }
                }
            }
        }

        public override void OnBeingPlayed(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                foreach (var card in playerCards.Board)
                {
                     card.CurrentHP=-1;   
                }
                foreach (var card in enemyCards.Board)
                {
                    card.CurrentHP=-1;   
                }
            }
        }
    }
}

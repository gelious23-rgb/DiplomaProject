using System;
using Script.Characters.Player;
using Script.Logic;
using Script.Spawner;
using UnityEngine;

namespace Script.Card.CardEffects
{
    public class LonginusEffect : Effect
    {
        public override void DoOnEnable()
        {
            Debug.Log("Longinus listener added");
            CardEffectHandler.OnDeath.AddListener(OnDeath);
        }

        public void OnDeath(CardInfoDisplay self)
        {
            if (self == GetCard())
            {
                Debug.Log("Longinus effect");
                var lance = CardEffectHandler.GetLibrary().AllAllCards[7];
             
           
            
                var player = GameObject.Find("Player").GetComponent<PlayerSpawnerCards>();
                var enemy = GameObject.Find("Enemy").GetComponent<EnemySpawnerCards>();
                if (player.Board.Contains(this.GetCard()))
                {
                    var lanceObj  =Instantiate(player.cardPref, player.PlayerHand);
                    var lanceSC = lanceObj.GetComponent<CardInfoDisplay>();
                    lanceSC.CharacterCard = lance;
                    lanceSC.Start();
                    lanceSC.OnTurnStart();
                    lanceSC.ShowCardInfoClientRpc(lance, true);
                    player.PlayerHandCards.Add(lanceSC);
                
                }
                else if (enemy.Board.Contains(this.GetCard()))
                {
                    var lanceObj  =Instantiate(enemy.cardPref, GameObject.Find("Enemy Hand").transform);
                    var lanceSC = lanceObj.GetComponent<CardInfoDisplay>();
                    lanceSC.CharacterCard = lance;
                    lanceSC.Start();
                    lanceSC.OnTurnStart();
                   // lanceSC.ShowCardInfo(lance, false);
                    enemy.EnemyHandCards.Add(lanceSC);
                
                }
            }
        }

        protected override void OnTurnEnd()
        {
            base.OnTurnEnd();
          //  CardEffectHandler.OnDeath.RemoveListener(OnDeath);
        }
    }
}

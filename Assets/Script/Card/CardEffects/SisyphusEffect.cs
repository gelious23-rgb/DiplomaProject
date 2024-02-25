using UnityEngine;

namespace Script.Card.CardEffects
{
    public class SisyphusEffect : Effect
    {
        public void SisyphusRevive(CardInfoDisplay sacrifice)
        {
            if (sacrifice != null)
            {
                GetCard().HP = GetCard().CharacterCard.hp;
                sacrifice.HP = 0;
                BattleBehaviour.CheckAliveEnemyCardOnBoard(sacrifice);
                BattleBehaviour.CheckAlivePlayerCardOnBoard(sacrifice);
                BattleBehaviour.CardDeath.DestroyCard(sacrifice);
                Debug.Log("Sacrifice is "+sacrifice.CharacterCard.name);
                GetCard().RefreshData();
            }

            if (sacrifice == null)
            {
                GetCard().HP = 0;
                BattleBehaviour.CheckAlivePlayerCardOnBoard(GetCard());
                BattleBehaviour.CheckAliveEnemyCardOnBoard(GetCard());
                BattleBehaviour.CardDeath.DestroyCard(GetCard());
                Debug.Log("Sisyphus is dead");
            }
        }

        
    }
}

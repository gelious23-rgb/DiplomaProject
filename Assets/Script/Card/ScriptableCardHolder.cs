using System;
using System.Collections;
using System.Collections.Generic;
using Script.Card.CardEffects;
using Script.Spawner;
using Script.UI.Text;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Card
{
    public class ScriptableCardHolder : MonoBehaviour
    {
        public List<Card> AllCards = new List<Card>();
        public DescExplanations keywords; 
        public List<Card> AllAllCards = new List<Card>();
         


        public void Start()
        {
            StartCoroutine(StartCor());
            Debug.Log("STart");
            
        }

        private IEnumerator StartCor()
        {
            yield return null;
            CardEffectHandler.OnTurnStart.Invoke();
        }
         [ContextMenu("Give Debug Card")]
        private void GiveCardToPlayerHandDebug()
        {
            var sp = FindObjectOfType<PlayerSpawnerCards>();

            var  Carddobj = Instantiate(sp.cardPref, sp.PlayerHand.transform);
            var Cardd = Carddobj.GetComponent<CardInfoDisplay>();
            Cardd.owner = sp; 
            Cardd.CharacterCard = AllAllCards[7]; 
            Cardd.OnTurnStart(); 
            Cardd.RefreshData(); 
            Cardd.ShowCardInfo(Cardd.CharacterCard, true); 
            sp.PlayerHandCards.Add(Cardd);
        }
        public void GiveCardToPlayerHand(int index)
        {
            var sp = FindObjectOfType<PlayerSpawnerCards>();

            var  Carddobj = Instantiate(sp.cardPref, sp.PlayerHand.transform);
            var Cardd = Carddobj.GetComponent<CardInfoDisplay>();
            Cardd.owner = sp;
            Cardd.CharacterCard = AllCards[index];
            Cardd.OnTurnStart();
            Cardd.RefreshData();
            Cardd.ShowCardInfo(Cardd.CharacterCard, true);
            sp.PlayerHandCards.Add(Cardd);
        }
        public void GiveCardToEnemyHand(int index)
        {
            
            var sp = FindObjectOfType<EnemySpawnerCards>();

            var  Carddobj = Instantiate(sp.cardPref, sp.EnemyHand.transform);
            var Cardd = Carddobj.GetComponent<CardInfoDisplay>();
            Cardd.owner = sp;
            Cardd.CharacterCard = AllCards[index];
            Cardd.OnTurnStart();
            Cardd.RefreshData();
            Cardd.ShowCardInfo(Cardd.CharacterCard, false );
            sp.EnemyHandCards.Add(Cardd);
        }
       
    }
}

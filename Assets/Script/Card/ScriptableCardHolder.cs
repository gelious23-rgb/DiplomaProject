using System;
using System.Collections;
using System.Collections.Generic;
using Script.Card.CardEffects;
using Script.Spawner;
using Script.UI.Text;
using Unity.VisualScripting;
using UnityEngine;

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
          Cardd.CharacterCard = AllCards[7];
          Cardd.OnTurnStart();
          Cardd.RefreshData();
          Cardd.ShowCardInfo(Cardd.CharacterCard, true);
          sp.Board.Add(Cardd);
        }
    }
}

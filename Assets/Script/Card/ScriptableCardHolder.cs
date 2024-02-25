using System;
using System.Collections;
using System.Collections.Generic;
using Script.Card.CardEffects;
using Script.UI.Text;
using UnityEngine;

namespace Script.Card
{
    public class ScriptableCardHolder : MonoBehaviour
    {
        public List<Card> AllCards = new List<Card>();
        public DescExplanations keywords; 
        public List<Card> AllAllCards = new List<Card>();
        [SerializeReference]
        internal List<Effect> AddABLE_Effects = new List<Effect>();


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
    }
}

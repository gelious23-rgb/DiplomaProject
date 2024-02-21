using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Card.CardDeck
{
    public abstract class CardDeckInstance
    {
        public List<Card> EnemyDeck;
        public List<Card> PlayerDeck;

        protected List<Card> GiveDeckCard()
        {
           // Debug.Log(GetCardLibrary().AllCards.Count + "All cards count");
            List<Card> list = new List<Card>();
            for(int i = 0; i < GetCardLibrary().AllCards.Count; i ++)
            {
                list.Add(GetCardLibrary().AllCards[Random.Range(0, GetCardLibrary().AllCards.Count)]);
            }
            return list;
        }

        private ScriptableCardHolder GetCardLibrary()
        {
           var  library = GameObject.FindGameObjectWithTag("CardLibrary");

           return library.GetComponent<ScriptableCardHolder>();
        }
    }
}
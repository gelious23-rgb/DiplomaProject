using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Card.CardDeck
{
    public abstract class CardDeckInstance
    {
        public List<int> EnemyDeck;
        public List<int> PlayerDeck;

        protected List<int> GiveDeckCard()
        {
           // Debug.Log(GetCardLibrary().AllCards.Count + "All cards count");
            List<int> list = new List<int>();
            for(int i = 0; i < GetCardLibrary().AllCards.Count; i ++)
            {
                list.Add(Random.Range(0, GetCardLibrary().AllCards.Count));
            }
            return list;
        }

        public ScriptableCardHolder GetCardLibrary()
        {
            var  library = GameObject.FindGameObjectWithTag("CardLibrary");
            return library.GetComponent<ScriptableCardHolder>();
        }
    }
}
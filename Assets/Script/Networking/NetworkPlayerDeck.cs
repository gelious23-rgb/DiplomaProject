using Script.Card;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Networking
{
    public class NetworkPlayerDeck
    {
               
        public List<Card.Card> PlayerDeck;
        
        public NetworkPlayerDeck()
        {
            PlayerDeck = GiveDeckCard();
        }


        private List<Card.Card> GiveDeckCard()
        {
            // Debug.Log(GetCardLibrary().AllCards.Count + "All cards count");
            List<Card.Card> list = new List<Card.Card>();
            for(int i = 0; i < GetCardLibrary().AllCards.Count; i ++)
            {
                list.Add(GetCardLibrary().AllCards[Random.Range(0, GetCardLibrary().AllCards.Count)]);
            }
            return list;
        }

        private NetworkScriptableCardHolder GetCardLibrary()
        {
            var  library = GameObject.FindGameObjectWithTag("CardLibrary");

            return library.GetComponent<NetworkScriptableCardHolder>();
        }
    }
}

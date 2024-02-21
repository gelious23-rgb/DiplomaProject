using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Card.CardDeck
{
    public abstract class CardDeckInstance
    {
        public List<CharacterCard> EnemyDeck;
        public List<CharacterCard> PlayerDeck;

        protected List<CharacterCard> GiveDeckCard()
        {
            List<CharacterCard> list = new List<CharacterCard>();
            for(int i = 0; i < 15; i ++)
            {
                list.Add(CardInstance.AllCards[Random.Range(0, CardInstance.AllCards.Count)]);
            }
            return list;
        }
    }
}
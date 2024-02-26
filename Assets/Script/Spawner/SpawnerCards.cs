using Script.Card;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Spawner
{
    public abstract class SpawnerCards : NetworkBehaviour
    {
        public GameObject cardPref;



        protected abstract void GiveStartCards(List<Card.Card> deck, Transform hand);
  


        protected abstract void GiveCardToHand(List<Card.Card> deck, Transform hand);

        public abstract void GiveNewCards();
        protected abstract void  SetupCard(Card.Card characterCard, Transform hand);

        protected abstract bool LogAndBurnCardsIfHandIsFull(List<Card.Card> deck, Transform hand);

        protected abstract void LogAndBurnCard(List<Card.Card> deck, string message);


    }
}
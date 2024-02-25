using System;
using Script.Card;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Spawner
{
    public abstract class SpawnerCards : MonoBehaviour
    {
        public GameObject cardPref;
        [SerializeField]public List<CardInfoDisplay> Board = new List<CardInfoDisplay>();

        public CardInfoDisplay GetCardOfType(Card.Card.Types type)
        {
            foreach (var cardInfo in Board)
            {
                if (cardInfo.CharacterCard.CardType == type)
                {
                    return cardInfo;
                }
            }

            return null;
        }



        protected abstract void GiveStartCards(List<Card.Card> deck, Transform hand);
  


        protected abstract void GiveCardToHand(List<Card.Card> deck, Transform hand);

        public abstract void GiveNewCards();
        protected abstract void  SetupCard(Card.Card characterCard, Transform hand);

        protected abstract bool LogAndBurnCardsIfHandIsFull(List<Card.Card> deck, Transform hand);

        protected abstract void LogAndBurnCard(List<Card.Card> deck, string message);


    }
}
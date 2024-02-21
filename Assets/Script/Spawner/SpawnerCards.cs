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



        protected abstract void GiveStartCards(List<CharacterCard> deck, Transform hand);
  


        protected abstract void GiveCardToHand(List<CharacterCard> deck, Transform hand);

        public abstract void GiveNewCards();
        protected abstract void  SetupCard(CharacterCard characterCard, Transform hand);

        protected abstract bool LogAndBurnCardsIfHandIsFull(List<CharacterCard> deck, Transform hand);

        protected abstract void LogAndBurnCard(List<CharacterCard> deck, string message);


    }
}
using Script.Card;
using Script.UI.Text;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Networking
{
    public class NetworkScriptableCardHolder : MonoBehaviour
    {
        public List<Card.Card> AllCards = new List<Card.Card>();

        public void GiveCardToPlayerHand(int index)
        {
            var sp = FindObjectOfType<NetworkPlayerSpawnerCards>();

            var  Carddobj = Instantiate(sp.cardPref, sp.PlayerHand.transform);
            var Cardd = Carddobj.GetComponent<CardInfoDisplay>();
            Cardd.NetworkPlayerSpawnerCards = sp;
            Cardd.CharacterCard = AllCards[index];
            Cardd.OnTurnStart();
            Cardd.RefreshData();
            Cardd.ShowCardInfo(Cardd.CharacterCard, true);
            sp.PlayerHandCards.Add(Cardd);
        }
    }
}

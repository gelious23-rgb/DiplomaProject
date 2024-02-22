using System.Collections;
using System.Collections.Generic;
using Script.Card.CardEffects;
using UnityEngine;

namespace Script.Card
{
    public class ScriptableCardHolder : MonoBehaviour
    {
        public List<Card> AllCards = new List<Card>();

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

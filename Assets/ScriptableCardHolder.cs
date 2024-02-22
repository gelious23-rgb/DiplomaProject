using System;
using System.Collections;
using System.Collections.Generic;
using Script.Card;
using Script.Card.CardEffects;
using UnityEngine;

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

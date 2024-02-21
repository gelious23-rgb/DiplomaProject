using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Card.CardEffects
{
    public static class CardEffectHandler
    {
        public static List<Effect> Effects = new List<Effect>();
        
        public static UnityEvent OnTurnStart = new UnityEvent();
        public static UnityEvent OnTurnEnd = new UnityEvent();
        public static UnityEvent<CardInfoDisplay> OnAttack = new UnityEvent<CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay> OnBeingHit = new UnityEvent<CardInfoDisplay>();

    }
}

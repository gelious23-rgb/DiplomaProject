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
<<<<<<< HEAD
        public static UnityEvent<CardInfoDisplay, CardInfoDisplay> OnAttack = new UnityEvent<CardInfoDisplay, CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay, CardInfoDisplay> OnBeingHit = new UnityEvent<CardInfoDisplay,CardInfoDisplay>();
=======
        public static UnityEvent<CardInfoDisplay> OnAttack = new UnityEvent<CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay> OnBeingHit = new UnityEvent<CardInfoDisplay>();
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d

    }
}

using System.Collections.Generic;
using Script.Logic;
using UnityEngine;
using UnityEngine.Events;

namespace Script.Card.CardEffects
{
    public static class CardEffectHandler
    {
        public static List<Effect> Effects = new List<Effect>();
        
        public static UnityEvent OnTurnStart = new UnityEvent();
        public static UnityEvent OnTurnEnd = new UnityEvent();
        public static UnityEvent<CardInfoDisplay, CardInfoDisplay> OnAttack = new UnityEvent<CardInfoDisplay, CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay, CardInfoDisplay> OnBeingHit = new UnityEvent<CardInfoDisplay,CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay, CardInfoDisplay> OnBeingHitAfter = new UnityEvent<CardInfoDisplay,CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay> OnDeath = new UnityEvent<CardInfoDisplay>();
        public static UnityEvent<CardInfoDisplay> OnBeingPlayed = new UnityEvent<CardInfoDisplay>();

         

        public static ScriptableCardHolder GetLibrary()
        {
            return GameObject.Find("CardLibrary").GetComponent<ScriptableCardHolder>();
        }

        public static Effect GetEffectByKeyword(string desc, List<Effect> List)
        {
            foreach (var keyword in GetLibrary().keywords.Descs)
            {
                if (desc.Contains(keyword) && keyword != "Counterattack" && keyword != "Protection")
                {
                    return List.Find(effect => effect.name==keyword);
                }
            }

            return null;
        }


    }
}

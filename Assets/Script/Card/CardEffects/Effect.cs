using System;

using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Effect : Card
    {
        public delegate void EffectHandler();
        public event EffectHandler OnTurnEnd;
    }
}

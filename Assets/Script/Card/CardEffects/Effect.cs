using System;

using UnityEngine;

namespace Script.Card.CardEffects
{
    public class Effect : CardScriptable
    {
        public delegate void EffectHandler();
        public event EffectHandler OnTurnEnd;
    }
}

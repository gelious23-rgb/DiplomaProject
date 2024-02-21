using Script.Game;
using System;

using UnityEngine;


namespace Script.Card.CardEffects
{
    public class Effect : MonoBehaviour
    {
        public BattleBehaviour BattleBehaviour;
        void Start()
        {
            CardEffectHandler.Effects.Add(this);
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
            CardEffectHandler.OnTurnEnd.AddListener(OnTurnEnd);
            CardEffectHandler.OnAttack.AddListener(OnAttack);
            CardEffectHandler.OnBeingHit.AddListener(OnBeingHit);
            
            BattleBehaviour = FindObjectOfType<BattleBehaviour>();
        }
        
        public CardInfoDisplay GetCard()
        {
            return GetComponent<CardInfoDisplay>();
        }
        protected virtual void OnAttack(CardInfoDisplay target)
        {
            throw new NotImplementedException();
        }
        protected virtual void OnBeingHit(CardInfoDisplay target)
        {
            throw new NotImplementedException();
        }
        protected virtual void OnTurnStart()
        {
            throw new NotImplementedException();
        }
        protected virtual void OnTurnEnd()
        {
            if (DestroyOnTurnEnd == true) 
            {
                
                /*Destroy(this);*/
            }
            
        }
        protected bool DestroyOnTurnEnd { get; set; }

    }
}

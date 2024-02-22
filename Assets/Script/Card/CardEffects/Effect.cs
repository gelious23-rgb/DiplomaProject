using Script.Game;
using System;

using UnityEngine;


namespace Script.Card.CardEffects
{
    public class Effect : MonoBehaviour
    {
        public BattleBehaviour BattleBehaviour;
        protected void Start()
        {
            CardEffectHandler.Effects.Add(this);
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
            CardEffectHandler.OnTurnEnd.AddListener(OnTurnEnd);
           // CardEffectHandler.OnAttack.AddListener(OnAttack);
          //  CardEffectHandler.OnBeingHit.AddListener(OnBeingHit); 
        }

        protected void OnEnable()
        {
            BattleBehaviour = FindObjectOfType<BattleBehaviour>();
        }

        public CardInfoDisplay GetCard()
        {
            return GetComponent<CardInfoDisplay>();
        }

        public virtual void OnAttack(CardInfoDisplay target, CardInfoDisplay self)
        {
            Debug.Log("On attack worked");
        }

        public virtual void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            throw new NotImplementedException();
        }
        protected virtual void OnTurnStart()
        {
            Debug.Log("On turn start worked");
        }
        protected virtual void OnTurnEnd()
        {
            if (DestroyOnTurnEnd == true) 
            {
                
                Destroy(this);
            }
            
        }
        protected bool DestroyOnTurnEnd { get; set; }

    }
}

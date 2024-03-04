using Script.Game;
using System;

using UnityEngine;
using UnityEngine.Serialization;


namespace Script.Card.CardEffects
{
     
    public class Effect : MonoBehaviour
    {
        public BattleBehaviour BattleBehaviour;
        public bool destroyOnTurnEnd=false;
        protected void Start()
        {
            if (CardEffectHandler.Effects.Contains(this) == false)
            {
                CardEffectHandler.Effects.Add(this);
            }
            
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
            CardEffectHandler.OnTurnEnd.AddListener(OnTurnEnd);
            CardEffectHandler.OnBeingPlayed.AddListener(OnBeingPlayed);
           // CardEffectHandler.OnAttack.AddListener(OnAttack);
          //  CardEffectHandler.OnBeingHit.AddListener(OnBeingHit); 
        }

        public virtual void OnBeingPlayed(CardInfoDisplay self)
        {
             
        }

        protected void OnEnable()
        {
            BattleBehaviour = FindObjectOfType<BattleBehaviour>();
            DoOnEnable();
        }

        public virtual void DoOnEnable()
        {
            
        }


        public CardInfoDisplay GetCard()
        {
            return GetComponent<CardInfoDisplay>();
        }

        public virtual void OnAttack(CardInfoDisplay self, CardInfoDisplay target)
        {
            Debug.Log("On attack worked");
        }

        public virtual void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
        {
            Debug.Log("On being hit worked");

        }
        protected virtual void OnTurnStart()
        {
            Debug.Log("On turn start worked");
        }
        protected virtual void OnTurnEnd()
        {
          if(destroyOnTurnEnd){Destroy(this);}

        }
       

    }
}

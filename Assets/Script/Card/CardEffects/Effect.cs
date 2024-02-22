using Script.Game;
using System;

using UnityEngine;


namespace Script.Card.CardEffects
{
    public class Effect : MonoBehaviour
    {
        public BattleBehaviour BattleBehaviour;
<<<<<<< HEAD
        protected void Start()
=======
        void Start()
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d
        {
            CardEffectHandler.Effects.Add(this);
            CardEffectHandler.OnTurnStart.AddListener(OnTurnStart);
            CardEffectHandler.OnTurnEnd.AddListener(OnTurnEnd);
<<<<<<< HEAD
           // CardEffectHandler.OnAttack.AddListener(OnAttack);
          //  CardEffectHandler.OnBeingHit.AddListener(OnBeingHit); 
        }

        protected void OnEnable()
        {
            BattleBehaviour = FindObjectOfType<BattleBehaviour>();
        }

=======
            CardEffectHandler.OnAttack.AddListener(OnAttack);
            CardEffectHandler.OnBeingHit.AddListener(OnBeingHit);
            
            BattleBehaviour = FindObjectOfType<BattleBehaviour>();
        }
        
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d
        public CardInfoDisplay GetCard()
        {
            return GetComponent<CardInfoDisplay>();
        }
<<<<<<< HEAD

        public virtual void OnAttack(CardInfoDisplay target, CardInfoDisplay self)
        {
            Debug.Log("On attack worked");
        }

        public virtual void OnBeingHit(CardInfoDisplay target, CardInfoDisplay damageSource)
=======
        protected virtual void OnAttack(CardInfoDisplay target)
        {
            throw new NotImplementedException();
        }
        protected virtual void OnBeingHit(CardInfoDisplay target)
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d
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
                
<<<<<<< HEAD
                Destroy(this);
=======
                /*Destroy(this);*/
>>>>>>> 6af6e68b54f96baeaf263203283ac3c9dbdd654d
            }
            
        }
        protected bool DestroyOnTurnEnd { get; set; }

    }
}

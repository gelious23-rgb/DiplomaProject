using System;
using Script.Card.CardEffects;
using Script.Characters.Enemy;
using Script.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



namespace Script.Card
{
    public class CardAttack : MonoBehaviour, IDropHandler
    {
        private EnemyAI _enemyAI;
        private BattleBehaviour _battleBehaviour; 

        private TurnBehaviour _turnBehaviour;

        private void Start()
        {
            _battleBehaviour = FindObjectOfType<BattleBehaviour>();
            _turnBehaviour = FindObjectOfType<TurnBehaviour>();
            _enemyAI = FindObjectOfType<EnemyAI>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var cardInfoScript = GetComponent<CardInfoDisplay>();
            
            if (!_turnBehaviour.IsPlayerTurn.Value)
                return;

            CardInfoDisplay cardInfoDis = eventData.pointerDrag.GetComponent<CardInfoDisplay>();

            if(cardInfoDis && cardInfoDis.CanAttack && transform.parent == _enemyAI.EnemyField)
            {
                cardInfoDis.ChangeAttackState(false);
                
                if (cardInfoDis.IsPlayer)
                    cardInfoDis.DeHighlightCard();

                _battleBehaviour.CardAttacking(cardInfoScript,cardInfoDis);
            }
            else if (cardInfoDis.CharacterCard.CardType == Card.Types.Tool)
            {
                if (cardInfoScript.owner == cardInfoDis.owner)
                {
                    var tooleffect = cardInfoDis.CharacterCard.Effects[0].GetComponent<Effect>();
                    var placeHolderEffect = CloneEffect(tooleffect);
                    var placeHolderEffect1 = (Effect)cardInfoScript.gameObject.AddComponent(placeHolderEffect.GetType());
                    foreach (var field in placeHolderEffect1.GetType().GetFields())
                    {
                        field.SetValue(placeHolderEffect1, field.GetValue(tooleffect));
                    }
                    placeHolderEffect1.enabled = false;
                    placeHolderEffect1.enabled = true;

                    _battleBehaviour.CardDeath.DestroyCard(cardInfoDis);
                }
            }
             

        }
        public  Effect CloneEffect(Effect original)
        {
            var clone = (Effect)original.gameObject.GetComponent(original.GetType());

            // Copy all the properties
            foreach (var field in original.GetType().GetFields())
            {
                field.SetValue(clone, field.GetValue(original));
            }

            return clone;
        }
    }
}

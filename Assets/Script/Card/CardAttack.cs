using Script.Characters.Enemy;
using Script.Game;
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
            
            if (!_turnBehaviour.IsPlayerTurn)
                return;

            CardInfoDisplay cardInfoDis = eventData.pointerDrag.GetComponent<CardInfoDisplay>();

            if(cardInfoDis && cardInfoDis.CharacterCard.CanAttack && transform.parent == _enemyAI.EnemyField)
            {
                cardInfoDis.CharacterCard.ChangeAttackState(false);
                
                if (cardInfoDis.IsPlayer)
                    cardInfoDis.DeHighlightCard();

                _battleBehaviour.CardsFight(cardInfoDis,cardInfoScript);
            }

        }
    }
}

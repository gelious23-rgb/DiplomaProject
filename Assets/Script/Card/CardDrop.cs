using Script.Characters;
using Script.Characters.Player;
using Script.Game;
using Script.Spawner;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


namespace Script.Card
{
    public class CardDrop : MonoBehaviour, IDropHandler , IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField]
        private CharacterFieldType characterFieldType;
        public CharacterFieldType Type => characterFieldType;
        private TurnBehaviour _turnBehaviour;

        private PlayerMana _playerMana;
        private PlayerSpawnerCards _playerSpawnerCards;

        
        private void Start()
        {
            _turnBehaviour = FindObjectOfType<TurnBehaviour>();
            _playerMana = FindObjectOfType<PlayerMana>();
            _playerSpawnerCards = FindObjectOfType<PlayerSpawnerCards>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (!IsPlayerBoard()) return;

            CardMove cardMove = eventData.pointerDrag.GetComponent<CardMove>();
            CardInfoDisplay cardInfo = cardMove.GetComponent<CardInfoDisplay>();

            if(cardMove && _playerSpawnerCards.PlayerFieldCards.Count < 6 && _turnBehaviour.IsPlayerTurn && _playerMana.CurrentPlayerMana >=
               cardInfo.CharacterCard.Manacost && !cardMove.GetComponent<CardInfoDisplay>().CharacterCard.IsPlaced)
            { 
                _playerSpawnerCards.PlayerHandCards.Remove(cardInfo);
                _playerSpawnerCards.PlayerFieldCards.Add(cardInfo);
                cardMove.DefaultParent = transform;

                cardInfo.CharacterCard.IsPlaced = true;
                _playerMana.ReduceMana(cardInfo.CharacterCard.Manacost);
               _turnBehaviour.CheckCardsForAvailability();
            }

        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null || Type == CharacterFieldType.ENEMY_BOARD ||  Type == CharacterFieldType.ENEMY_HAND || Type == CharacterFieldType.PLAYER_HAND)
                return;

            CardMove cardMove = eventData.pointerDrag.GetComponent<CardMove>();

            if (cardMove)
                cardMove.DefaultTempCardParent = transform;

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            CardMove card = eventData.pointerDrag.GetComponent<CardMove>();

            if (card && card.DefaultTempCardParent == transform)
                card.DefaultTempCardParent = card.DefaultParent;

        }
        private bool IsPlayerBoard() => Type == CharacterFieldType.PLAYER_BOARD;
    }
}
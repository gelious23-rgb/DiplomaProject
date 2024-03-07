using Script.Card.CardEffects;
using Script.Characters;
using Script.Characters.Player;
using Script.Game;
using Script.Spawner;
using Unity.Netcode;
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

            if (cardInfo.CharacterCard.CardType != Card.Types.Tool)
            {
                var isPlayerTurn = (_turnBehaviour.IsPlayerTurn.Value && NetworkManager.Singleton.IsHost)
                                   || (!_turnBehaviour.IsPlayerTurn.Value && !NetworkManager.Singleton.IsHost);
                if(cardMove && _playerSpawnerCards.Board.Count < 6 && isPlayerTurn && _playerMana.CurrentPlayerMana >=
                    cardInfo.CharacterCard.manacost && !cardMove.GetComponent<CardInfoDisplay>().IsPlaced)
                {
                    _playerSpawnerCards.PlayerHandCards.Remove(cardInfo);
                    _playerSpawnerCards.Board.Add(cardInfo);
                    //CardEffectHandler.OnBeingPlayed.Invoke(cardInfo);
                    cardMove.DefaultParent = transform;

                    cardInfo.IsPlaced = true;
                    if (cardInfo.owner.gameObject.GetComponent<NetworkObject>().NetworkManager.IsHost)
                    {
                        _turnBehaviour.ReduceHostMana(cardInfo.CharacterCard.manacost);
                    }
                    else
                    {
                        _turnBehaviour.ReduceClientManaServerRpc(cardInfo.CharacterCard.manacost);
                    }
                    _turnBehaviour.CheckCardsForAvailability();
                }
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
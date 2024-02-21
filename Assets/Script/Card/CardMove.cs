using Script.Characters;
using Script.Characters.Player;
using Script.Game;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Card
{
    public class CardMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        private Camera _mainCamera;
        private Vector3 _offset;
        private Transform _defaultParent,_defaultTempCardParent;
        private bool _isDraggable;
        
        [SerializeField] private GameObject _tempCardGo;

        [SerializeField] private CardInfoDisplay CardInfoDisplay;

        private PlayerMana _playerMana;
        private TurnBehaviour _turnBehaviour;

        private TurnBehaviour TurnBehaviour => _turnBehaviour;

        private PlayerMana PlayerMana => _playerMana;
        public Transform DefaultParent
        {
            get => _defaultParent;
            set => _defaultParent = value;
        }

        public Transform DefaultTempCardParent
        {
            get => _defaultTempCardParent;
            set => _defaultTempCardParent = value;
        }
        private void Awake()
        {
            _mainCamera = Camera.allCameras[0];
            _tempCardGo = GameObject.Find("TempCardGO");
            _turnBehaviour = FindObjectOfType<TurnBehaviour>();
            _playerMana = FindObjectOfType<PlayerMana>();

        }
        
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _offset = transform.position - _mainCamera.ScreenToWorldPoint(eventData.position);
            _defaultParent = _defaultTempCardParent = transform.parent;
            SetDraggableStatus();

            if (!_isDraggable)
                return;

            HighlightTargetsIfCanAttack();
            SetTempCardParent();
            
            transform.SetParent(_defaultParent.parent);
            
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        private void SetDraggableStatus()
        {
            CardDrop cardDrop = DefaultParent.GetComponent<CardDrop>();
            _isDraggable = TurnBehaviour.IsPlayerTurn && (IsPlayerHandWithEnoughMana(cardDrop) || IsPlayerBoardWithAttackCapability(cardDrop));
        }



        private void HighlightTargetsIfCanAttack()
        {
            if(CardInfoDisplay.CharacterCard.CanAttack)
                TurnBehaviour.HighlightTargets(true);
        }

        private void SetTempCardParent()
        {
            _tempCardGo.transform.SetParent(_defaultParent);
            _tempCardGo.transform.SetSiblingIndex(transform.GetSiblingIndex());
        }


        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDraggable)
                return;
            NewCarPos(eventData);
            if (_tempCardGo.transform.parent != _defaultTempCardParent)
                _tempCardGo.transform.SetParent(_defaultTempCardParent);
            
            if(IsPlayerBoard())
                CheckPosition();
        }

        private void NewCarPos(PointerEventData eventData)
        {
            Vector3 newPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            transform.position = newPos + _offset;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isDraggable)
                return;
            
            _turnBehaviour.HighlightTargets(false);

            transform.SetParent(_defaultParent);

            GetComponent<CanvasGroup>().blocksRaycasts = true;
            SetToCanvas();
        }
        private void SetToCanvas()
        {
            transform.SetSiblingIndex(_tempCardGo.transform.GetSiblingIndex());
            _tempCardGo.transform.SetParent(GameObject.Find("Canvas").transform);
            _tempCardGo.transform.localPosition = new Vector3(3055, 0);
        }

        private void CheckPosition()
        {
            var newIndex = _defaultTempCardParent.childCount;

            for(int i = 0; i < _defaultTempCardParent.childCount; i ++)
            {
                if(transform.position.x < _defaultTempCardParent.GetChild(i).position.x)
                {
                    if (_tempCardGo.transform.GetSiblingIndex() < i) newIndex--;
                    break;
                }
            }
            _tempCardGo.transform.SetSiblingIndex(newIndex);
        }

        private bool IsPlayerBoardWithAttackCapability(CardDrop cardDrop) => cardDrop.Type == CharacterFieldType.PLAYER_BOARD &&
                                                                             CardInfoDisplay.CharacterCard.CanAttack;
        private bool IsPlayerBoard() => _defaultParent.GetComponent<CardDrop>().Type != CharacterFieldType.PLAYER_BOARD;
        private bool IsPlayerHandWithEnoughMana(CardDrop cardDrop) => cardDrop.Type == CharacterFieldType.PLAYER_HAND &&
                                                                      PlayerMana.CurrentPlayerMana >= CardInfoDisplay.CharacterCard.Manacost;



    }

}

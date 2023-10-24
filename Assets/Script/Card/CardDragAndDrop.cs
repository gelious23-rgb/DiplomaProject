using Script.Characters;
using Script.Game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Card
{
    public class CardDragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler 
    {
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Vector3 _originalPosition;
        private GameObject _playerHand;
        private GameObject _playerBoard;
        private GameObject _enemyHand;
        private GameObject _enemyBoard;
        private GameObject _gameScene;
        private Transform _currentParent;
        private int _boardCardLimitCount;
        private BoardManager _boardManager;
        private Mana.Mana _mana;
        private int _cardDataManacost;


        public void Initialize(GameObject hand, GameObject board, GameObject enemy_hand, GameObject enemy_board, GameObject scene, int boardCardLimit, Player player,
            Mana.Mana mana, int cardDataManacost)
        {
            _playerHand = hand;
            _playerBoard = board;
            _enemyBoard = enemy_board;
            _enemyHand = enemy_hand;
            _gameScene = scene;
            _boardCardLimitCount = boardCardLimit;
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _mana = mana;
            _cardDataManacost = cardDataManacost;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            

            _currentParent = _rectTransform.parent;
            _rectTransform.SetParent(_gameScene.transform);

            _canvasGroup.blocksRaycasts = false;

            _originalPosition = _rectTransform.position;
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Drag ended over: " + eventData.pointerEnter);

            // If the card's original parent was the enemy's hand or board, return it to its original position
            if (_currentParent == _enemyHand.transform || _currentParent == _enemyBoard.transform)
            {
                _rectTransform.position = _originalPosition;
                _rectTransform.SetParent(_currentParent);
                _canvasGroup.blocksRaycasts = true;
                return;
            }

            // Check if the card is dragged from the player hand and dropped on the player board
            if (eventData.pointerEnter != null && eventData.pointerEnter.transform == _playerBoard.transform && _currentParent.transform == _playerHand.transform)
            {
                if (_playerBoard.transform.childCount < _boardCardLimitCount && _mana.ManaCurrent >= _cardDataManacost)
                {
                    _rectTransform.SetParent(_playerBoard.transform);
                    _mana.Decrease(_cardDataManacost);
                }
                else
                {
                    if (_mana.ManaCurrent < _cardDataManacost)
                    {
                        Debug.Log("Not Enough Mana");
                    }
                    else if (_playerBoard.transform.childCount >= _boardCardLimitCount)
                    {
                        Debug.Log("Board is Full");
                    }

                    _rectTransform.position = _originalPosition;
                    _rectTransform.SetParent(_currentParent.transform);
                }
            }

            // Check if the card was dragged from the player board
            else if (_currentParent.transform == _playerBoard.transform)
            {
                _rectTransform.position = _originalPosition;
                _rectTransform.SetParent(_currentParent.transform);
            }
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
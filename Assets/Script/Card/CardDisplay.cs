using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Card
{
    public class CardDisplay : MonoBehaviour
    {
        [SerializeField]
        public global::Script.Card.Card Card;
        [SerializeField]
        private TextMeshProUGUI _cardName;
        [SerializeField]
        private TextMeshProUGUI _cardDescription;
        [SerializeField]
        private TextMeshProUGUI _cardHp;
        [SerializeField]
        private TextMeshProUGUI _cardAttack;
        [SerializeField]
        private TextMeshProUGUI _cardManacost;
        [SerializeField]
        private TextMeshProUGUI _cardType;
        [SerializeField]
        private Image _cardImage;
        



        void Start()
        {
            CardInitialize();
        }

        private void CardInitialize()
        {
            _cardName.text = Card.Name;
            _cardDescription.text = Card.Description;
            _cardType.text = Card.CardType.ToString();

            _cardAttack.text = Card.Attack.ToString();
            _cardManacost.text = Card.Manacost.ToString();
            _cardHp.text = Card.Hp.ToString();

            _cardImage.sprite = Card.CardImage;
            _cardImage.color = new Color32(255, 255, 255, 255);

        }
        
    }
    
}

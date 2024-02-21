
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card
{
    public class CardInfoDisplay : MonoBehaviour
    {

        public CharacterCard CharacterCard;
        [SerializeField]
        private Image _sprite;
        [SerializeField]
        private TextMeshProUGUI _name, _description, _type;
        [SerializeField]
        private TextMeshProUGUI _attack, _hp, _manacost;
        
        [SerializeField]
        private GameObject _hideGO,_highliter;
        public bool IsPlayer;
        [SerializeField]
        private Color  _targetColor;
        [SerializeField]
        private Color _normalColor;
        public Image _Image;

        public void ShowCardInfo(CharacterCard characterCard, bool isPlayer)
        {
            IsPlayer = isPlayer;
            _hideGO.SetActive(false);

            CharacterCard = characterCard;

            _sprite.sprite = characterCard.Image;
            _sprite.preserveAspect = true;
            _name.text = characterCard.Name;
            _description.text = characterCard.Description;
            _type.text = characterCard.Type;
        
        

            RefreshData();
        }

        public void HideCardInfo(CharacterCard characterCard)
        {
            CharacterCard = characterCard;
            _hideGO.SetActive(true);
            IsPlayer = false;
        }

        public void RefreshData()
        {
            _attack.text = CharacterCard.Damage.ToString();
            _hp.text = CharacterCard.Hp.ToString();
            _manacost.text = CharacterCard.Manacost.ToString();
        }



        
        public void HighlightCard() => _highliter.SetActive(true);

        public void DeHighlightCard() => _highliter.SetActive(false);

        public void CheckForAvailability(int currentMana)
        {
            if (GetComponent<CanvasGroup>() == null)
            {
                Debug.Log("_canvasGroup == null");
            }
            else
            {
                GetComponent<CanvasGroup>().alpha = currentMana >= CharacterCard.Manacost ? 1 : .5f;
            }

        }

        public void HighlightAsTarget(bool highlight) => _Image.color = highlight ? _targetColor : _normalColor;

    }
}

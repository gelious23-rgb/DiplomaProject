using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Script.Card;

public class CardInfoScript : MonoBehaviour
{
    
    public Card _selfCard;
    [SerializeField]
    private Image _sprite;
    [SerializeField]
    private TextMeshProUGUI _name, _description, _type;
    [SerializeField]
    private TextMeshProUGUI _attack, _hp, _manacost;
    [SerializeField]
    private GameObject _hideGO,_highliter;
    public bool isPlayer;
    [SerializeField]
    private Color  _targetColor;
    [SerializeField]
    private Color _normalColor;

    public GameObject Highliter
    {
        get
        {
            return _highliter;
        }
        set
        {
            _highliter = value;
        }
    }

    public void ShowCardInfo(Card card, bool p_isPlayer)
    {
        isPlayer = p_isPlayer;
        _hideGO.SetActive(false);

        _selfCard = card;

        _sprite.sprite = card.image;
        _sprite.preserveAspect = true;
        _name.text = card.name;
        _description.text = card.description;
        _type.text = card.type;
        
        

        RefreshData();
    }

    public void HideCardInfo(Card card)
    {
        _selfCard = card;
        _hideGO.SetActive(true);
        isPlayer = false;
    }

    public void RefreshData()
    {
        _attack.text = _selfCard.damage.ToString();
        _hp.text = _selfCard.hp.ToString();
        _manacost.text = _selfCard.manacost.ToString();
    }

    public void HighlightCard()
    {
        _highliter.SetActive(true);
    }

    public void DeHighlightCard()
    {
        _highliter.SetActive(false);
    }

    public void CheckForAvailability(int p_currentMana)
    {
        GetComponent<CanvasGroup>().alpha = p_currentMana >= _selfCard.manacost ? 1 : .5f;
    }

    public void HighlightAsTarget(bool p_highlite)
    {
        

        GetComponent<Image>().color = p_highlite ? _targetColor : _normalColor;
    }
}

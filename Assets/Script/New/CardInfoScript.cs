using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CardInfoScript : MonoBehaviour
{
    
    public Card _selfCard;
    [SerializeField]
    private Image _sprite;
    [SerializeField]
    private TextMeshProUGUI _name, _description, _type;
    [SerializeField]
    private TextMeshProUGUI _attack, _hp, _manacost;

    public void ShowCardInfo(Card card)
    {
        _selfCard = card;

        _sprite.sprite = card.image;
        _sprite.preserveAspect = true;
        _name.text = card.name;
        _description.text = card.description;
        _type.text = card.type;
        _attack.text = card.damage.ToString();
        _hp.text = card.hp.ToString();
        _manacost.text = card.manacost.ToString();
    }

    public void HideCardInfo(Card card)
    {
        _selfCard = card;
        _sprite.sprite = null;
        _name.text = _description.text = _type.text = "";
    }

    private void Start()
    {
       // ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}

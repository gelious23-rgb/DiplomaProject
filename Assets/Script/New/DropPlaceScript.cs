using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum FieldType
{
    PLAYER_HAND,
    PLAYER_BOARD,
    ENEMY_HAND,
    ENEMY_BOARD
}

public class DropPlaceScript : MonoBehaviour, IDropHandler , IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private FieldType _fieldType;
    public FieldType Type
    {
        get
        {
            return _fieldType;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (Type != FieldType.PLAYER_BOARD)
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if(card && card.GameManager.PlayerFieldCards.Count < 6 && card.GameManager.IsPlayerTurn && card.GameManager.PlayerMana >=
            card.GetComponent<CardInfoScript>()._selfCard.manacost)
        {
            card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScript>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScript>());
            card.DefaultParent = transform;

            card.GameManager.ReduceMana(true,card.GetComponent<CardInfoScript>()._selfCard.manacost);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || Type == FieldType.ENEMY_BOARD ||  Type == FieldType.ENEMY_HAND || Type == FieldType.PLAYER_HAND)
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        CardMovementScript card = eventData.pointerDrag.GetComponent<CardMovementScript>();

        if (card && card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefaultParent;
    }
}

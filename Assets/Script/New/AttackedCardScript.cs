using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCardScript : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!GetComponent<CardMovementScript>().GameManager.IsPlayerTurn)
            return;

        CardInfoScript card = eventData.pointerDrag.GetComponent<CardInfoScript>();

        if(card && card._selfCard.canAttack && transform.parent == GetComponent<CardMovementScript>().GameManager.enemyField)
        {
            card._selfCard.ChangeAttackState(false);

            if (card.isPlayer)
                card.DeHighlightCard();

            GetComponent<CardMovementScript>().GameManager.CardsFight(card,GetComponent<CardInfoScript>());
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    private GameManagerScript _gm;

    public void MakeTurn()
    {
        StartCoroutine(EnemyTurn(_gm.EnemyHandCards));
    }

    IEnumerator EnemyTurn(List<CardInfoScript> cards)
    {
        yield return new WaitForSeconds(1);

        int count = cards.Count == 1 ? 1 :
            Random.Range(0, cards.Count);

        for (int i = 0; i < count; i++)
        {
            if (_gm.EnemyFieldCards.Count > 5 || _gm.EnemyMana == 0 || _gm.EnemyHandCards.Count == 0)
                break;

            List<CardInfoScript> cardList = cards.FindAll(x => _gm.EnemyMana >= x._selfCard.manacost);

            if (cardList.Count == 0)
                break;

            cardList[0].GetComponent<CardMovementScript>().MovetoField(_gm.enemyField);

            _gm.ReduceMana(false, cardList[0]._selfCard.manacost);

            yield return new WaitForSeconds(.51f);

            cardList[0].ShowCardInfo(cardList[0]._selfCard, false);
            cardList[0].transform.SetParent(_gm.enemyField);

            _gm.EnemyFieldCards.Add(cardList[0]);
            _gm.EnemyHandCards.Remove(cardList[0]);
        }

        yield return new WaitForSeconds(1);

        foreach (var activeCard in _gm.EnemyFieldCards.FindAll(x => x._selfCard.canAttack))
        {
            if (_gm.PlayerFieldCards.Count != 0)
            {
                var enemy = _gm.PlayerFieldCards[Random.Range(0, _gm.PlayerFieldCards.Count)];

                Debug.Log(activeCard._selfCard.name + " ( " + activeCard._selfCard.damage + ";" + activeCard._selfCard.hp +
                     " --> " + enemy._selfCard.name + " ( " + enemy._selfCard.damage + ";" + enemy._selfCard.hp);


                activeCard._selfCard.ChangeAttackState(false);

                activeCard.GetComponent<CardMovementScript>().MovetoTarget(enemy.transform);
                yield return new WaitForSeconds(.75f);

                _gm.CardsFight(enemy, activeCard);
            }

            yield return new WaitForSeconds(.2f);

        }
        yield return new WaitForSeconds(1);
        _gm.ChangeTurn();
    }
}

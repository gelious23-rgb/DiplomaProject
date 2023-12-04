using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Game
{
    public List<Card> enemyDeck, playerDeck;

    public Game()
    {
        enemyDeck = GiveDeckCard();
        playerDeck = GiveDeckCard();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for(int i = 0; i < 15; i ++)
        {
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        }
        return list;
    }
}

public class GameManagerScript : MonoBehaviour
{
    public Game currentGame;
    public Transform enemyHand, playerHand, enemyField, playerField;
    public GameObject cardPref;
    private int _turn, _turnTime = 30;
    private int _maxMana = 1;
    private const int maxPlayerHandSize = 6;
    private const int maxEnemyHandSize = 6;


    private int _playerMana = 1, _enemyMana = 1;

    public int PlayerMana
    {
        get
        {
            return _playerMana;
        }
    }

    public int EnemyMana
    {
        get
        {
            return _enemyMana;
        }
    }

    public List<CardInfoScript> PlayerHandCards = new List<CardInfoScript>(),
                                PlayerFieldCards = new List<CardInfoScript>(),
                                EnemyHandCards = new List<CardInfoScript>(),
                                EnemyFieldCards = new List<CardInfoScript>();

    public bool IsPlayerTurn
    {
        get
        {
            return _turn % 2 == 0;
        }
    }

    private void Awake()
    {
        UIController.Instance.HideInterfaceText();
    }

    private void Start()
    {
        _turn = 0;

        currentGame = new Game();

        GiveStartCards(currentGame.enemyDeck, enemyHand);
        GiveStartCards(currentGame.playerDeck, playerHand);

        UIController.Instance.ShowMana();

        StartCoroutine(TurnFunc());
    }

    void GiveStartCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(deck, hand);
    }

    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;
        if (hand == enemyHand && EnemyHandCards.Count >= maxEnemyHandSize)
        {
            // Log the card that is burned when the enemy hand is full
            Debug.Log("Enemy's hand is full. Burning card: " + deck[0].name);
            deck.RemoveAt(0);
            return;
        }

        if (hand != enemyHand && PlayerHandCards.Count >= maxPlayerHandSize)
        {
            // Log the card that is burned when the player hand is full
            Debug.Log("Player's hand is full. Burning card: " + deck[0].name);
            deck.RemoveAt(0);
            return;
        }

        Card card = deck[0];

        GameObject cardGO = Instantiate(cardPref, hand, false);

        if (hand == enemyHand)
        {
            cardGO.GetComponent<CardInfoScript>().HideCardInfo(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScript>());
        }
        else
        {
            cardGO.GetComponent<CardInfoScript>().ShowCardInfo(card, true);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScript>());
            cardGO.GetComponent<AttackedCardScript>().enabled = false;
        }

        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc()
    {
        _turnTime = 30;
        UIController.Instance.TurnTimeText.text = _turnTime.ToString();

        foreach (var card in PlayerFieldCards)
        {
            card.DeHighlightCard();
        }

        CheckCardsForAvaliability();

        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card._selfCard.ChangeAttackState(true);
                card.HighlightCard();
            }

            while (_turnTime-- > 0)
            {
                UIController.Instance.TurnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            ChangeTurn();
        }
        else
        {
            foreach (var card in EnemyFieldCards)
                card._selfCard.ChangeAttackState(true);

            
           StartCoroutine(EnemyTurn(EnemyHandCards));
            
        }

       
    }

    private int CalculateDamageToEnemyPlayerForActiveCards()
    {
        int damageDealt = 0;

        foreach (var activeCard in PlayerFieldCards.FindAll(x => x._selfCard.canAttack))
        {
            damageDealt += activeCard._selfCard.manacost;
        }

        return damageDealt;
    }

    private int CalculateDamageToPlayerForActiveCards()
    {
        int damageDealt = 0;

        foreach (var activeCard in EnemyFieldCards.FindAll(x => x._selfCard.canAttack))
        {
            damageDealt += activeCard._selfCard.manacost;
        }

        return damageDealt;
    }

    IEnumerator EnemyTurn(List<CardInfoScript> cards)
    {
        yield return new WaitForSeconds(1);

        int count = cards.Count == 1 ? 1 :
            Random.Range(0, cards.Count);

        for (int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5 || _enemyMana == 0 || EnemyHandCards.Count == 0)
                break;

            List<CardInfoScript> cardList = cards.FindAll(x => _enemyMana >= x._selfCard.manacost);

            if (cardList.Count == 0)
                break;

            cardList[0].GetComponent<CardMovementScript>().MovetoField(enemyField);

            ReduceMana(false, cardList[0]._selfCard.manacost);

            yield return new WaitForSeconds(.51f);

            cardList[0].ShowCardInfo(cardList[0]._selfCard, false);
            cardList[0].transform.SetParent(enemyField);

            EnemyFieldCards.Add(cardList[0]);
            EnemyHandCards.Remove(cardList[0]);
        }

        yield return new WaitForSeconds(1);

        foreach (var activeCard in EnemyFieldCards.FindAll(x => x._selfCard.canAttack))
        {
            if (PlayerFieldCards.Count != 0)
            {
                var enemy = PlayerFieldCards[Random.Range(0, PlayerFieldCards.Count)];

                Debug.Log(activeCard._selfCard.name + " ( " + activeCard._selfCard.damage + ";" + activeCard._selfCard.hp +
                     " --> " + enemy._selfCard.name + " ( " + enemy._selfCard.damage + ";" + enemy._selfCard.hp);


                activeCard._selfCard.ChangeAttackState(false);

                activeCard.GetComponent<CardMovementScript>().MovetoTarget(enemy.transform);
                yield return new WaitForSeconds(.75f);

                CardsFight(enemy, activeCard);
            }

            yield return new WaitForSeconds(.2f);

        }
        yield return new WaitForSeconds(1);
        ChangeTurn();
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();

        // Check if the current player has active cards and the opponent player has no cards on their board
        if (IsPlayerTurn)
        {
            if (PlayerFieldCards.Exists(x => x._selfCard.canAttack) && EnemyFieldCards.Count == 0)
            {
                int damageDealt = CalculateDamageToEnemyPlayerForActiveCards();
                if (damageDealt > 0)
                {
                    Debug.Log("Player 2 takes " + damageDealt + " damage from active cards.");
                    DealDamageToEnemyHero(damageDealt);
                }
            }
        }
        else
        {
            if (EnemyFieldCards.Exists(x => x._selfCard.canAttack) && PlayerFieldCards.Count == 0)
            {
                int damageDealt = CalculateDamageToPlayerForActiveCards();
                if (damageDealt > 0)
                {
                    Debug.Log("Player 1 takes " + damageDealt + " damage from active cards.");
                    DealDamageToPlayerHero(damageDealt);
                }
            }
        }

        _turn++;

        UIController.Instance.EndTurnButton.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveNewCards();
            _maxMana = Mathf.Min(_maxMana + 1, 10);
            _playerMana = _enemyMana = _maxMana;
            UIController.Instance.ShowMana();
        }

        StartCoroutine(TurnFunc());
    }

    private void GiveNewCards()
    {
        GiveCardToHand(currentGame.enemyDeck, enemyHand);
        GiveCardToHand(currentGame.playerDeck, playerHand);
    }

    public void CardsFight(CardInfoScript playerCard, CardInfoScript enemyCard)
    {
        if (IsPlayerTurn)
        {
            //playerCard._selfCard.GetDamage(enemyCard._selfCard.damage);
            enemyCard._selfCard.GetDamage(playerCard._selfCard.damage);
        }
        else
        {
            playerCard._selfCard.GetDamage(enemyCard._selfCard.damage);
            // enemyCard._selfCard.GetDamage(playerCard._selfCard.damage);
        }


        if (!playerCard._selfCard.isAlive)
        {
            DestroyCard(playerCard);
            int damageDealt = playerCard._selfCard.manacost;
            DealDamageToPlayerHero(damageDealt);
        }
        else
            playerCard.RefreshData();

        if (!enemyCard._selfCard.isAlive)
        {
            DestroyCard(enemyCard);
            int damageDealt = enemyCard._selfCard.manacost;
            DealDamageToEnemyHero(damageDealt);
        }
        else
            enemyCard.RefreshData();

    }

    public void DestroyCard(CardInfoScript card)
    {
        card.GetComponent<CardMovementScript>().OnEndDrag(null);

        if (EnemyFieldCards.Exists(x => x == card))
            EnemyFieldCards.Remove(card);

        if (PlayerFieldCards.Exists(x => x == card))
            PlayerFieldCards.Remove(card);

        Destroy(card.gameObject);
    }

    

    public void ReduceMana(bool p_playerMana, int p_manacost)
    {
        if (p_playerMana)
            _playerMana = Mathf.Clamp(_playerMana - p_manacost, 0, int.MaxValue);
        else
            _enemyMana = Mathf.Clamp(_enemyMana - p_manacost, 0, int.MaxValue);

        UIController.Instance.ShowMana();
    }

    private void DealDamageToEnemyHero(int damage)
    {
        // Reduce the enemy hero's health and update the UI
        int currentHealth = int.Parse(UIController.Instance.enemyHealthText.text);
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            UIController.Instance.GameOver(true);
        }
        UIController.Instance.enemyHealthText.text = currentHealth.ToString();
    }

    private void DealDamageToPlayerHero(int damage)
    {
        // Reduce the player hero's health and update the UI
        int currentHealth = int.Parse(UIController.Instance.playerHealthText.text);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            UIController.Instance.GameOver(false);
        }
        UIController.Instance.playerHealthText.text = currentHealth.ToString();
    }

    
    public void RestartGame()
    {
        
        Time.timeScale = 1;

        string currentSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(currentSceneName);
    }

    public void CheckCardsForAvaliability()
    {
        foreach (var card in PlayerHandCards)
            card.CheckForAvailability(PlayerMana);
    }

    public void HighliteTargets(bool p_highlite)
    {
        foreach (var card in EnemyFieldCards)
            card.HighlightAsTarget(p_highlite);
    }
}


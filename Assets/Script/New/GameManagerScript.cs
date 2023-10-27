using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private TextMeshProUGUI _turnTimeText;
    [SerializeField]
    private Button _endTurnButton;
    [SerializeField]
    private Button _restartTurnButton;
    [SerializeField]
    private TextMeshProUGUI _playerGameOver,_enemyGameOver;


    public int playerMana = 10, enemyMana = 10;
    [SerializeField]
    private TextMeshProUGUI _playerManaText, _enemyManaText, _playerHealthText, _enemyHealthText;
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
        _restartTurnButton.gameObject.SetActive(false);
        _playerGameOver.gameObject.SetActive(false);
        _enemyGameOver.gameObject.SetActive(false);
    }

    private void Start()
    {
        _turn = 0;

        currentGame = new Game();

        GiveStartCards(currentGame.enemyDeck, enemyHand);
        GiveStartCards(currentGame.playerDeck, playerHand);

        ShowMana();

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
        _turnTimeText.text = _turnTime.ToString();

        foreach (var card in PlayerFieldCards)
        {
            card.DeHighlightCard();
        }

        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card._selfCard.ChangeAttackState(true);
                card.HighlightCard();
            }

            while (_turnTime-- > 0)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            foreach (var card in EnemyFieldCards)
                card._selfCard.ChangeAttackState(true);

            while (_turnTime-- > 27)
            {
                _turnTimeText.text = _turnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            if (EnemyHandCards.Count > 0)
            {
                EnemyTurn(EnemyHandCards);
            }
        }

        ChangeTurn();
    }

    private void EnemyTurn(List<CardInfoScript> cards)
    {
        int count = cards.Count == 1 ? 1 :
            Random.Range(0, cards.Count);

        for (int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5 || enemyMana == 0)
                return;

            List<CardInfoScript> cardList = cards.FindAll(x => enemyMana >= x._selfCard.manacost);

            if (cardList.Count == 0)
                return;

            ReduceMana(false, cardList[0]._selfCard.manacost);

            cardList[0].ShowCardInfo(cardList[0]._selfCard, false);
            cardList[0].transform.SetParent(enemyField);

            EnemyFieldCards.Add(cardList[0]);
            EnemyHandCards.Remove(cardList[0]);
        }

        foreach (var activeCard in EnemyFieldCards.FindAll(x => x._selfCard.canAttack))
        {
            if (PlayerFieldCards.Count == 0)
            {
                return;// Deal damage to player equel to manacosts;
            }

            var enemy = PlayerFieldCards[Random.Range(0, PlayerFieldCards.Count)];

            Debug.Log(activeCard._selfCard.name + " ( " + activeCard._selfCard.damage + ";" + activeCard._selfCard.hp +
                 " --> " + enemy._selfCard.name + " ( " + enemy._selfCard.damage + ";" + enemy._selfCard.hp);


            activeCard._selfCard.ChangeAttackState(false);
            CardsFight(enemy, activeCard);
        }
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        _turn++;

        _endTurnButton.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
        {
            GiveNewCards();
            playerMana = enemyMana = 10;
            ShowMana();
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

    public void ShowMana()
    {
        _playerManaText.text = playerMana.ToString();
        _enemyManaText.text = enemyMana.ToString();
    }

    public void ReduceMana(bool p_playerMana, int p_manacost)
    {
        if (p_playerMana)
            playerMana = Mathf.Clamp(playerMana - p_manacost, 0, int.MaxValue);
        else
            enemyMana = Mathf.Clamp(enemyMana - p_manacost, 0, int.MaxValue);

        ShowMana();
    }

    private void DealDamageToEnemyHero(int damage)
    {
        // Reduce the enemy hero's health and update the UI
        int currentHealth = int.Parse(_enemyHealthText.text);
        currentHealth -= damage;
        if(currentHealth <=0)
        {
            GameOver(true);
        }
        _enemyHealthText.text = currentHealth.ToString();
    }

    private void DealDamageToPlayerHero(int damage)
    {
        // Reduce the player hero's health and update the UI
        int currentHealth = int.Parse(_playerHealthText.text);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameOver(false);
        }
        _playerHealthText.text = currentHealth.ToString();
    }

    private void GameOver(bool p_bool)
    {
        Time.timeScale = 0;

        if (p_bool)
            _playerGameOver.gameObject.SetActive(true);
        else
            _enemyGameOver.gameObject.SetActive(true);

        _restartTurnButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        // Unpause the game
        Time.timeScale = 1;

        // Get the name of the currently loaded scene (assuming it's the scene you want to restart)
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Reload the current scene
        SceneManager.LoadScene(currentSceneName);
    }
}


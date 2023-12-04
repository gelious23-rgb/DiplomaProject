using Script.Health;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField]
    private GameManagerScript _gm;
    
    public TextMeshProUGUI playerGameOver, enemyGameOver;
    [SerializeField]
    private TextMeshProUGUI _turnTimeText;
    [SerializeField]
    private Button _endTurnButton;
    [SerializeField]
    private Button _restartTurnButton;
    

    public TextMeshProUGUI playerManaText, enemyManaText, playerHealthText, enemyHealthText;

    public TextMeshProUGUI TurnTimeText
    {
        get
        {
            return _turnTimeText; 
        }
    }
    public Button EndTurnButton
    {
        get
        {
            return _endTurnButton;
        }
    }
    public Button RestartTurnButton
    {
        get
        {
            return _restartTurnButton;
        }
    }

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }

    public void HideInterfaceText()
    {
        _restartTurnButton.gameObject.SetActive(false);
        playerGameOver.gameObject.SetActive(false);
        enemyGameOver.gameObject.SetActive(false);
    }

    public void GameOver(bool p_bool)
    {
        Time.timeScale = 0;

        if (p_bool)
            playerGameOver.gameObject.SetActive(true);
        else
            enemyGameOver.gameObject.SetActive(true);

         _restartTurnButton.gameObject.SetActive(true);
    }

    public void ShowMana()
    {
        playerManaText.text = _gm.PlayerMana.ToString();
        enemyManaText.text = _gm.EnemyMana.ToString();
    }
}

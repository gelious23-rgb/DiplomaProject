using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject LobbyPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private Toggle PlayerOneReady;
    [SerializeField] private Toggle PlayerTwoReady;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private TextMeshProUGUI readyOneText;
    [SerializeField] private TextMeshProUGUI readyTwoText;

    private bool showNotReadyMessage = false;

    public void Start()
    {
        PlayerOneReady.isOn = false;
        PlayerTwoReady.isOn = false;
        UpdateStartButtonInteractivity();
    }

    

    public void TogglePlayerOneReady()
    {
        UpdateStartButtonInteractivity();

        if (PlayerOneReady.isOn == false) 
        {
            readyOneText.text = "Not Ready";
        }
        else
        {
            readyOneText.text = "Ready";
        }
    }

    public void TogglePlayerTwoReady()
    {
        UpdateStartButtonInteractivity();

        if (PlayerTwoReady.isOn == false)
        {
            readyTwoText.text = "Not Ready";
        }
        else
        {
            readyTwoText.text = "Ready";
        }
    }

    private void UpdateStartButtonInteractivity()
    {
        bool playerOneReady = PlayerOneReady.isOn;
        bool playerTwoReady = PlayerTwoReady.isOn;

        bool canStartGame = playerOneReady && playerTwoReady;

        if (!canStartGame)
        {
            SetPromptText("Both players are not ready");
            showNotReadyMessage = true;
        }
        else if (showNotReadyMessage)
        {
            SetPromptText("");
            showNotReadyMessage = false;
        }
    }

    public void StartGame()
    {
        bool canStartGame = PlayerOneReady.isOn && PlayerTwoReady.isOn;

        if (!canStartGame)
        {
            SetPromptText("Cannot start game. Both players are not ready.");
            StartCoroutine(ClearPromptAfterDelay(3f));
        }
        else
        {
            Debug.Log("Start game");
            SceneManager.LoadScene(2);
            // Here's code for starting the game
        }
    }
    public void Settings()
    {
        LobbyPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        SetPromptText("");
        showNotReadyMessage = false;
    }

    public void Back()
    {
        SettingsPanel.SetActive(false);
        LobbyPanel.SetActive(true);
        UpdateStartButtonInteractivity();
    }
    public void Leave()
    {
        SceneManager.LoadScene(0);
        Debug.Log("ChangeScenes");
    }
    private void SetPromptText(string message)
    {
        promptText.text = message;
    }

    private IEnumerator ClearPromptAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetPromptText("Both players are not ready");
    }
}

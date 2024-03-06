using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject JoinPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject CompediumPanel;

    public void FindGame()
    {
        MenuPanel.SetActive(false);
        JoinPanel.SetActive(true);
    }
    public void JoinGame()
    {
        //Join Game script
        Debug.Log("Join game");
    }
    public void BacktoLobby()
    {
        JoinPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void SettingsButton()
    {
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        
    }
    public void BackSettings()
    {
        SettingsPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
    public void Compedium()
    {
        MenuPanel.SetActive(false);
        CompediumPanel.SetActive(true);
    }
    public void CompediumBack()
    {
        CompediumPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }
    public void CreateGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}

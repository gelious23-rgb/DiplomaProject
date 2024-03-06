using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Lobby
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject MenuPanel;
        [SerializeField] private GameObject JoinPanel;
        [SerializeField] private GameObject SettingsPanel;

        public void CreateGame()
        {
            SceneManager.LoadScene("Lobby Scene");
        }
        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
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
            //Compedium script
        }
        public void RunSinglePlayer()
        {
            SceneManager.LoadScene(2);
        }

    }
}

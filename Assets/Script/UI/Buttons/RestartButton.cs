using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.UI.Buttons
{
    public class RestartButton : MonoBehaviour
    {
        public void RestartGame()
        { 
            Time.timeScale = 1;
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }

    }
}
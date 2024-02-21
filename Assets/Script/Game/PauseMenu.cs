using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject popupSurrenderMenu;
    public GameObject popupQuitMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Zatrzymuje czas gry, aby wszystko siê zatrzyma³o
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }    

    public void SettingsBack()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void PopUpSurrender()
    {
        pauseMenuUI.SetActive(false);
        popupSurrenderMenu.SetActive(true);
    }
    public void SurrenderBack()
    {
        popupSurrenderMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Surrender()
    {
        Debug.Log("Surrender");
        Time.timeScale = 1f;
    }

    public void PopUpQuit()
    {
        pauseMenuUI.SetActive(false);
        popupQuitMenu.SetActive(true);
    }
    public void QuitBack()
    {
        popupQuitMenu.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        SceneManager.LoadScene(0);
    }
}

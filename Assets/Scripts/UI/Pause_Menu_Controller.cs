using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public string MainMenuSceneName;

    public GameObject pauseMenu;
    private void Awake()
    {
        Game_Manager.OnGameStateChanged += SetPauseMenuActive;
        Game_Manager.OnGameStateChanged += SetPauseMenuDeactive;
    }

    private void OnDisable()
    {
        Game_Manager.OnGameStateChanged -= SetPauseMenuActive;
        Game_Manager.OnGameStateChanged -= SetPauseMenuDeactive;
    }

    private void SetPauseMenuActive(GameState gameState)
    {
        if (gameState == GameState.GamePaused)
        {
            pauseMenu.SetActive(true);
        }
    }

    private void SetPauseMenuDeactive(GameState gameState)
    {
        if (gameState == GameState.GameResumed)
        {
            pauseMenu.SetActive(false);
        }
    }
    public void OnClickSettings()
    {
        // Settings Menu Code Here
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

}

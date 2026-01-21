using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public string MainMenuSceneName;

    public GameObject pauseMenu;

    private GameObject pauseMenuReference;

    private void Awake()
    {
        pauseMenuReference = pauseMenu;
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
            pauseMenuReference.SetActive(true);
            Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.pause);
        }
    }

    private void SetPauseMenuDeactive(GameState gameState)
    {
        if (gameState == GameState.GameResumed)
        {
            pauseMenuReference.SetActive(false);
            Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.unpause);
        }
    }
    public void OnClickSettings()
    {
        // Settings Menu Code Here
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
    }

    public void OnClickMainMenu()
    {
        Game_Manager.Instance.UpdateGameState(GameState.GameResumed);
        Game_Manager.Instance.UpdateGameState(GameState.MainMenu);
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
        SceneManager.LoadScene(MainMenuSceneName);
    }

}

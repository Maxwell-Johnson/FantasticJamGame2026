using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboard_Buttons_Controller : MonoBehaviour
{

    public string MainMenuSceneName;
    public void OnClickRestart()
    {
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
        Game_Manager.Instance.RestartGame();
    }

    public void OnClickMainMenu()
    {
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
        Game_Manager.Instance.UpdateGameState(GameState.MainMenu);
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
        SceneManager.LoadScene(MainMenuSceneName);
    }
}

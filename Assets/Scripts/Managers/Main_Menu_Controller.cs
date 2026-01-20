using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{
    public string StartSceneName;
    public string LeaderboardsSceneName;
    public void OnStartClick()
    {
        Game_Manager.Instance.UpdateGameState(GameState.GameRunning);
        SceneManager.LoadScene(StartSceneName);
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnSettingsClick()
    {
        //Settings Menu Code
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
    }

    public void OnLeaderboardsClick()
    {
        if (LeaderboardsSceneName != null)
        {
            //SceneManager.LoadScene(LeaderboardsSceneName);
        }
        
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.buttonClick);
    }
}

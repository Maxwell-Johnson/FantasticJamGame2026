using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{
    public string StartSceneName;
    public string LeaderboardsSceneName;
    public void OnStartClick()
    {
        SceneManager.LoadScene(StartSceneName);
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
    }

    public void OnLeaderboardsClick()
    {
        SceneManager.LoadScene(LeaderboardsSceneName);
    }
}

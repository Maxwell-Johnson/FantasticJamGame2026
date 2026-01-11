using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Dan.Main;

public class Player_Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;
    private string publicLeaderboardKey = 
        "190acc66e8f864f83f6c6bb6821464743e769870e908a28f90ad8d8da6a4b377";
    public GameObject leaderboardScreenToggle;
    private Current_Score_Text_Updater scoreTextUpdater;
    public int placeHolderScoreReference = 10;
    public Text inputScore_varTypeSubjectToChange;



    private void Start()
    {
        Game_Manager.OnGameStateChanged += LoadLeaderboard;
        
    }

    private void OnDestroy()
    {
        Game_Manager.OnGameStateChanged -= LoadLeaderboard;
    }

    public void LoadLeaderboard(GameState gameState)
    {
        if (gameState == GameState.PlayerDead)
        {
            scoreTextUpdater = GameObject.FindGameObjectWithTag("Leaderboard Logic").GetComponent<Current_Score_Text_Updater>();
            getLeaderboard();
            loadLeaderboardScreen();
        }
    }

    // Fetches leaderboard data
    public void getLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int checkIfLeaderboardLessThanAmountOfEntries = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < checkIfLeaderboardLessThanAmountOfEntries; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    // uploads username(of up to 5 characters) and score to leaderboard
    public void setLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((msg) =>
        {
            username.Substring(0, 5);
            getLeaderboard();
            Debug.Log("Successfully added " + username + "'s score (" + score + ") to Leaderboard!");
        }));
    }

    // Remember to reference this whenever game over happens
    public void loadLeaderboardScreen()
    {
        leaderboardScreenToggle.SetActive(true);
        scoreTextUpdater.updateCurrentScore(inputScore_varTypeSubjectToChange, placeHolderScoreReference);

    }
}

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
    public Score_Manager scoreManager;
    public Text playerScoreText;
    private int currentPlayerScore;



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

    public void SendPlayerScore(int playerScore)
    {
        currentPlayerScore = playerScore;
    }

    // Fetches leaderboard data
    public void getLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int checkIfLeaderboardLessThanAmountOfEntries = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < checkIfLeaderboardLessThanAmountOfEntries; ++i)
            {
                // Strip hidden suffix if it exists
                string displayName = msg[i].Username;

                int suffixIndex = displayName.LastIndexOf('_');
                if (suffixIndex > 0 && displayName.Length - suffixIndex == 5) // underscore + 4 chars
                {
                    displayName = displayName.Substring(0, suffixIndex);
                }
                names[i].text = displayName;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    // uploads username(of up to 5 characters) and score to leaderboard
    public void setLeaderboardEntry(string username, int score)
    {
        // Ensure the displayed part is short (optional)
        string displayName = username.Length > 5 ? username.Substring(0, 5) : username;

        // Add a hidden suffix so the server treats it as a new player
        string hiddenSuffix = "_" + System.Guid.NewGuid().ToString("N").Substring(0, 4);
        string uploadName = displayName + hiddenSuffix;

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, uploadName, score, ((msg) =>
        {
            Leaderboards.FantasticGameJamLeaderboard.ResetPlayer();
            
            getLeaderboard();
            //Debug.Log("Successfully added " + username + "'s score (" + score + ") to Leaderboard!");
        }));
    }

    // Remember to reference this whenever game over happens
    public void loadLeaderboardScreen()
    {
        scoreTextUpdater.updateCurrentScore(playerScoreText, currentPlayerScore);
        leaderboardScreenToggle.SetActive(true);
        scoreManager.setScoreText(playerScoreText);
        scoreTextUpdater.updateCurrentScore(playerScoreText, currentPlayerScore);

    }
}

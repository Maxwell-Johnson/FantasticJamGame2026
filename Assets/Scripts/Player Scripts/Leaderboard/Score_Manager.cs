using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
public class Score_Manager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private TMP_InputField inputName;

    public Player_Leaderboard playerLeaderboard;

    public UnityEvent<string, int> submitScoreEvent;

    public void setScoreText(Text text)
    {
        scoreText = text;
    }
    public void submitScore()
    {
        playerLeaderboard.getLeaderboard();
        submitScoreEvent.Invoke(inputName.text, int.Parse(scoreText.text));
        playerLeaderboard.getLeaderboard();

    }

}

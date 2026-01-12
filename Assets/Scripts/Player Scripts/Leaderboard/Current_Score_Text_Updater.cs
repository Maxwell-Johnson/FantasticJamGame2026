using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Current_Score_Text_Updater : MonoBehaviour {

    public void updateCurrentScore(Text currentScore, int playerScore)
    {
        if (currentScore != null) {
            currentScore.text = playerScore.ToString();
        } else if (playerScore == null)
        {
            Debug.Log("placeholder is null :(");
        }
        {
            Debug.Log("score is null :(");
        }
    }
}

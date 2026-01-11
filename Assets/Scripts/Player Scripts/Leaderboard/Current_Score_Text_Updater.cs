using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Current_Score_Text_Updater : MonoBehaviour {

    public void updateCurrentScore(Text currentScore, int placeHolderScoreReference)
    {
        if (currentScore != null) {
            currentScore.text = placeHolderScoreReference.ToString();
        } else if (placeHolderScoreReference == null)
        {
            Debug.Log("placeholder is null :(");
        }
        {
            Debug.Log("score is null :(");
        }
    }
}

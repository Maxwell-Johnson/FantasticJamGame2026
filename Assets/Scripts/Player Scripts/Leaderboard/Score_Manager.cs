using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
public class Score_Manager : MonoBehaviour
{
    [SerializeField]
    private Text inputScore_varTypeSubjectToChange;
    [SerializeField]
    private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;
    public void submitScore()
    {
        submitScoreEvent.Invoke(inputName.text, int.Parse(inputScore_varTypeSubjectToChange.text));
    }

}

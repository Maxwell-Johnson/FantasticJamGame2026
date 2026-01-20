using UnityEngine;
using TMPro;
using static Game_Manager;

public class DistanceCounterScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI distanceText;
    private float distanceNum;

    // Update is called once per frame
    void Update()
    {
        if (Game_Manager.Instance.state == GameState.GameRunning)
        {
            distanceNum = Stats_Manager.Instance.distancesTravelled;
            distanceText.text = distanceNum.ToString("F0");

        }
    }
}

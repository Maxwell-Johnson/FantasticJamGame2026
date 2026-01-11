using UnityEngine;
using TMPro;
using static Game_Manager;

public class SkullCounterScript : MonoBehaviour
{

    private TextMeshProUGUI skullCounter;
    private int skullCountInt;

    private void Awake()
    {
        //Finds the text display component in one of the children objects
        skullCounter = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        //If the game is running, set the sjull count to whatever the STats_Manager says, convert to string, and display in the SkullCounter text
        if (Game_Manager.Instance.state == GameState.GameRunning)
        {
            skullCountInt = Stats_Manager.Instance.skullsCollected;
            skullCounter.text = skullCountInt.ToString();
        }
    }
}

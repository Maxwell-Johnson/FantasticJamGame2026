using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public float distanceTravelledInt = 2;
    public float distanceTravelledNum;
    public float speed = 1.5f;
    float secondsCounter;
    
    // Update is called once per frame
    void Update()
    {
        secondsCounter = Time.deltaTime * 100;

        if (secondsCounter >= 1)
        {
            distanceTravelledNum = (distanceTravelledInt * speed)/4;
            Stats_Manager.Instance.DistanceTravelled(distanceTravelledNum);
            secondsCounter = 0;

        }

    }
}

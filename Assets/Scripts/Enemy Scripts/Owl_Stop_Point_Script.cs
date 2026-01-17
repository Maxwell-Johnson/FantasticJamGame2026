using UnityEngine;

public class Owl_Stop_Point_Script : MonoBehaviour
{
    public bool spotOccupied = false;
    public int spotNumber;

    // -1.5 3.75
    public void occupySpace()
    {
        spotOccupied = true;
    }



}

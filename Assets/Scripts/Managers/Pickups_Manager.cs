using UnityEngine;

public class Pickups_Manager : MonoBehaviour
{

    public static Pickups_Manager Instance;

    public GameObject skullPickup;



    void Start()
    {
        Instance = this;
    }


    public void SpawnSkullPickup(Vector3 position, Quaternion rotation)
    {
        Instantiate(skullPickup, position, rotation);
        Debug.Log("SPAWN");
    }
}

using Unity.Cinemachine;
using UnityEngine;

public class Skull_Pickup : MonoBehaviour
{

    private int pickupValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Accesses global Stats Manager instance to add the value of this pickup to the global stat tracker for skulls
        if (collision.CompareTag("Pickupbox"))
        {
            Stats_Manager.Instance.SkullCollected(pickupValue);
            Destroy(gameObject);
        }
    }

}

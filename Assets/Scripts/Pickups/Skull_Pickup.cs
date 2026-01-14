using Unity.Cinemachine;
using UnityEngine;

public class Skull_Pickup : MonoBehaviour
{

    private int pickupValue = 1;
    private Rigidbody2D rb;
    private float fallSpeed = -3f;
    private float deadzone = -14f;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Accesses global Stats Manager instance to add the value of this pickup to the global stat tracker for skulls
        if (collision.CompareTag("Pickupbox"))
        {
            Stats_Manager.Instance.SkullCollected(pickupValue);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, fallSpeed);

        if (gameObject.transform.position.y <= deadzone)
        {
            Destroy(gameObject);
        }
    }

}

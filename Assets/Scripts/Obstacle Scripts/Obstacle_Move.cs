using UnityEngine;

public class Obstacle_Move : MonoBehaviour
{

    public float moveSpeed = 5;
    private float deadZone = -14;

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

        if (transform.position.y < deadZone) Destroy(gameObject); // Deletes objects once they go offscreen
    }
}

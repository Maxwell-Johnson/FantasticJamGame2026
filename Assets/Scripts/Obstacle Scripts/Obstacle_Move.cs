using UnityEngine;

public class Obstacle_Move : MonoBehaviour
{

    [Header("Obstacle Type")]
    [SerializeField] bool log;
    [SerializeField] bool boulder;

    public float boulderSetSpeed;
    public float moveSpeed;
    private float deadZone = -14f;

    private float maxFallSpeed = 10f;
    private float minFallSpeed = 0.8f;


    private void Awake()
    {
        moveSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        //Debug.Log(moveSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        if (log && !boulder)
        {
            transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

            if (transform.position.y < deadZone) Destroy(gameObject); // Deletes objects once they go offscreen
        }
        else if (boulder && !log)
        {
            transform.position = transform.position + (Vector3.down * boulderSetSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

            if (transform.position.y < deadZone) Destroy(gameObject); // Deletes objects once they go offscreen
        }
        else
        {
            Debug.Log("ERROR: OBSTACLE TYPE NOT SET");
        }
    }
}

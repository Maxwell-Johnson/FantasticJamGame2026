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
        Game_Manager.OnGameStateChanged += DestroySelf;
        moveSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        //Debug.Log(moveSpeed);
    }
    // Update is called once per frame

    private void OnDestroy()
    {
        Game_Manager.OnGameStateChanged -= DestroySelf;
    }
    void Update()
    {
        if (log && !boulder)
        {
            transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

            if (transform.position.y < deadZone) DestroySelf(Game_Manager.Instance.state); // Deletes objects once they go offscreen
        }
        else if (boulder && !log)
        {
            transform.position = transform.position + (Vector3.down * boulderSetSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

            if (transform.position.y < deadZone) DestroySelf(Game_Manager.Instance.state); // Deletes objects once they go offscreen
        }
        else
        {
            Debug.Log("ERROR: OBSTACLE TYPE NOT SET");
        }
    }


    private void DestroySelf(GameState gameState)
    {
        if (gameState == GameState.PlayerDead || gameState == GameState.GameReset || gameState == GameState.MainMenu)
        {
            Destroy(gameObject);
        }
    }
}

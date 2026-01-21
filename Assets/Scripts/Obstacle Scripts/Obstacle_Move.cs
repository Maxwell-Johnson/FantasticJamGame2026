using UnityEngine;

public class Obstacle_Move : MonoBehaviour
{

    [Header("Obstacle Type")]
    [SerializeField] bool log;
    [SerializeField] bool boulder;

    public float boulderSetSpeed = 3;
    public float moveSpeed;
    private float deadZone = -14f;

    private float maxFallSpeed = 8f;
    private float minFallSpeed = 1f;


    private void Awake()
    {
        Game_Manager.OnGameStateChanged += DestroySelf;
        moveSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        //Debug.Log(moveSpeed);

        if (boulder)
        {
            boulderSetSpeed = 3;    
            if (Stats_Manager.Instance.distancesTravelled >= 500 && Stats_Manager.Instance.distancesTravelled <= 10000) boulderSetSpeed = (17125 - Stats_Manager.Instance.distancesTravelled) / 2375;
            if (Stats_Manager.Instance.distancesTravelled > 10000) boulderSetSpeed = 7;
        }


    }

    private void OnDestroy()
    {
        Game_Manager.OnGameStateChanged -= DestroySelf;
    }
    void Update()
    {
        if (log && !boulder)
        {
            transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

            if (transform.position.y < deadZone) Destroy(gameObject); ; // Deletes objects once they go offscreen
        }
        else if (boulder && !log)
        {
            transform.position = transform.position + (Vector3.down * boulderSetSpeed) * Time.deltaTime; //Moves obstacles downward at a set move speed.

            if (transform.position.y < deadZone) Destroy(gameObject); ; // Deletes objects once they go offscreen
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


using UnityEngine;

public class Owl_Fire_Bolt : MonoBehaviour
{
    private Rigidbody2D rb;
    private int projectileDamage = 1;
    private Transform target;
    private float speed = 6f;
    private bool projectileOutOfBounds;
    private float xLeftBound = -7f;
    private float xRightBound = 10f;
    private float yNorthBound = 13f;
    private float ySouthBound = -14f;
    private Vector2 direction;



    private void Awake()
    {
        Game_Manager.OnGameStateChanged += DestroySelf;
    }

    private void OnDestroy()
    {
        Game_Manager.OnGameStateChanged -= DestroySelf;
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //projectileMovementSpotMultiplied = new Vector2(target.position.x * 10, target.position.y * 10);
        direction = target.transform.position - transform.position;
        direction = direction.normalized;
        rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    private void Update()
    {


        

        CheckIfOutOfBounds();

        if (projectileOutOfBounds)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (!playerHealth.playerTookDamage)
            {
                playerHealth.TakeDamage(projectileDamage);
                
            }

            

        }

        Destroy(rb);
        Destroy(gameObject);
    }



    private void CheckIfOutOfBounds()
    {
        if (gameObject.transform.position.x < xLeftBound || gameObject.transform.position.x > xRightBound)
        {
            Destroy(rb);
            Destroy(gameObject);
        }
        else if (gameObject.transform.position.y < ySouthBound || gameObject.transform.position.x > yNorthBound)
        {
            Destroy(rb);
            Destroy(gameObject);
        }

    }

    private void DestroySelf(GameState gameState)
    {
        if (gameState == GameState.PlayerDead || gameState == GameState.GameReset || gameState == GameState.MainMenu)
        {
            Destroy(rb);
            Destroy(gameObject);
        }
    }
}

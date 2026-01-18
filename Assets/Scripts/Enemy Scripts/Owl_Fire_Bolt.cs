using System.Xml.Xsl;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Owl_Fire_Bolt : MonoBehaviour
{
    private Rigidbody2D rb;
    private int projectileDamage = 1;
    private Transform target;
    private float speed = 6f;
    public Transform projectileTransform;
    private Vector2 projectileMovementSpotMultiplied;
    private bool projectileOutOfBounds;
    private float xLeftBound = -7f;
    private float xRightBound = 10f;
    private float yNorthBound = 13f;
    private float ySouthBound = -14f;
    private Vector2 direction;


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
        //projectileTransform.position = Vector2.MoveTowards(projectileTransform.position, projectileMovementSpotMultiplied, speed * Time.deltaTime);

        

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

            Destroy(gameObject);

        }
    }



    private void CheckIfOutOfBounds()
    {
        if (gameObject.transform.position.x < xLeftBound || gameObject.transform.position.x > xRightBound)
        {
            Destroy(gameObject);
        }
        else if (gameObject.transform.position.y < ySouthBound || gameObject.transform.position.x > yNorthBound)
        {
            Destroy(gameObject);
        }

    }

}

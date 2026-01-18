using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy_Movement : MonoBehaviour
{

    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Transform followTarget;
    Vector2 moveDirection;
    private bool takingKnockback;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
 
    }

    private void Start()
    {
        //Sets target to follow to the game object with the player tag
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Basically if you havea  follow target, it subtracts your psositon from the follow target position at a normalized rate to be used in fixed update
        if (followTarget)
        {
            Vector3 direction = (followTarget.position - transform.position).normalized;
            moveDirection = direction;

            /*  
            Use if you want the meleeEnemy to rotate in the direction of the player

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;

            */
        }
    }

    private void FixedUpdate()
    {
        //Uses the normalized direction to move object in the direction of the player by move speed
        if (followTarget && !takingKnockback)
        {
            rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    public void takingKnockbackTrue()
    {
        takingKnockback = true;
    }

    public void takingKnockbackFalse()
    {
        takingKnockback = false;
    }
}

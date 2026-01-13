using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Powerup_Pickup : MonoBehaviour
{




    private float deadzone = -14f;
    private Rigidbody2D rb;
    private float fallSpeed;
    private float fallSpeedMax = -4f;
    private float fallSpeedMin = -1f;

    private GameObject spawner;

    private int pickupNumber;
    private float powerupDuration;
    private bool powerupActive;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Powerup Spawner 1");

        rb = gameObject.GetComponent<Rigidbody2D>();

        initializePowerup();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Pickupbox"))
        {

            Powerups_Manager.Instance.activatePowerup(gameObject.tag);
            DeletePowerup();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, fallSpeed);


        if (gameObject.transform.position.y <= deadzone)
        {
            DeletePowerup();
        }
    }

    private void RemoveFromList()
    {
        spawner.GetComponent<Powerup_Spawner>().powerupList.Remove(gameObject);
    }

    private void DeletePowerup()
    {
        RemoveFromList();
        Destroy(gameObject);
    }

    private void initializePowerup()
    {
        fallSpeed = Random.Range(fallSpeedMin, fallSpeedMax);


        // Sets pickup to be one of 3 random pickups using tags
        pickupNumber = Random.Range(1, 4);
        if (pickupNumber == 1)
        {
            gameObject.tag = Powerups_Manager.Instance.powerupOne;
        }
        else if (pickupNumber == 2)
        {
            gameObject.tag = Powerups_Manager.Instance.powerupTwo;
        }
        else if (pickupNumber == 3)
        {
            gameObject.tag = Powerups_Manager.Instance.powerupThree;

        }

    }
}



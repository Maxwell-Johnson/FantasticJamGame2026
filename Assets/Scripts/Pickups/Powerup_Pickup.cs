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
    private SpriteRenderer spriteRend;

    private int powerupNumber;
    private float powerupDuration;
    private bool powerupActive;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Powerup Spawner 1");
        spriteRend = gameObject.GetComponent<SpriteRenderer>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        initializePowerup();

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Pickupbox"))
        {
            Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.powerUpCollect);
            Powerups_Manager.Instance.ActivatePowerup(gameObject.tag);
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
        if (gameObject != null)
        {
            spawner.GetComponent<Powerup_Spawner>().powerupList.Remove(gameObject);
        }
        
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
        powerupNumber = Random.Range(1, 4);
        if (powerupNumber == 1)
        {
            
            gameObject.tag = Powerups_Manager.Instance.powerupOne;
            
        }
        else if (powerupNumber == 2)
        {
            gameObject.tag = Powerups_Manager.Instance.powerupTwo;
        }
        else if (powerupNumber == 3)
        {
            gameObject.tag = Powerups_Manager.Instance.powerupThree;

        }

        Sprite spriteGrabbed = spawner.GetComponentInChildren<Sprite_Handler_Powerups>().GrabSprite(powerupNumber);

        spriteRend.sprite = spriteGrabbed;

    }


}



using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Powerups_Manager : MonoBehaviour
{

    public static Powerups_Manager Instance;

    public string powerupOne { get; private set; } = "Larger Attack Powerup";
    private float largerAttackDuration = 5f;
    public bool largerAttackIsActive { get; private set; }
    private float largerAttackActiveTimer;

    public string powerupTwo { get; private set; } = "Pickup 2";
    private float increasedDamageDuration = 5f;
    private bool increasedDamageIsActive;
    private float increasedDamageActiveTimer;
    private int increasedDamageMod = 2;
    private int originalDamageMod;

    public string powerupThree { get; private set; } = "Pickup 3";
    private float speedUpDuration = 10f;
    private bool speedUpIsActive;
    private float speedUpActiveTimer;
    private float speedUpMultiplier = 2f;
    private float originalSpeedX;
    private float originalSpeedY;

    public GameObject playerWeaponRegular;
    public GameObject playerWeaponLarger;
    public Weapon playerWeaponRegularScript;
    public Weapon playerWeaponLargerScript;
    public Player_Controller playerController;
    public SpriteRenderer playerWeaponRegularSpriteRend;
    public SpriteRenderer playerWeaponLargerSpriteRend;
    public Transform playerWeaponTransform;
    private Color originalWeaponColor;

    private void Awake()
    {
        Instance = this;
    }

    // In update, manager is constanely checking if any powerup active timer has a value over 0.
    // If it does, that means its active and will begin subtracting delta time from it (counting down)
    // Once it reaches at (or below) 0, it runs the revert function for that powerup.
    private void Update()
    {
        if (largerAttackActiveTimer > 0f)
        {
            largerAttackActiveTimer -= Time.deltaTime;

            if (largerAttackActiveTimer <= 0f)
            {
                Debug.Log("Poweup Has Ended!");
                LargerAttackRevert();
                largerAttackIsActive = false;
                //end powerup
            }
        }

        if (increasedDamageActiveTimer > 0f)
        {

            increasedDamageActiveTimer -= Time.deltaTime;

            if (increasedDamageActiveTimer <= 0f)
            {
                Debug.Log("Poweup Has Ended!");
                increasedDamageRevert();
                increasedDamageIsActive = false;
                //end powerup
            }
        }

        if (speedUpActiveTimer > 0f)
        {

            speedUpActiveTimer -= Time.deltaTime;

            if (speedUpActiveTimer <= 0f)
            {
                Debug.Log("Poweup Has Ended!");
                speedUpRevert();
                speedUpIsActive = false;
                //end powerup
            }
        }
    }
    public void ActivatePowerup(string powerupTag)
    {
        Debug.Log(powerupTag);
        if (powerupTag == powerupOne)
        {
           
            largerAttackActiveTimer += largerAttackDuration;
            
            if (!largerAttackIsActive)
            {
                Debug.Log("Activating Powerup!");
                LargerAttackPowerup();
                largerAttackIsActive = true;
            }
        }
        else if (powerupTag == powerupTwo)
        {
            increasedDamageActiveTimer += increasedDamageDuration;

            if (!increasedDamageIsActive)
            {
                Debug.Log("Activating Powerup!");
                increasedDamageEffect();
                increasedDamageIsActive = true;
            }

        }
        else if (powerupTag == powerupThree)
        {
            speedUpActiveTimer += speedUpDuration;

            if (!speedUpIsActive)
            {
                Debug.Log("Activating Powerup!");
                speedUpEffect();
                speedUpIsActive = true;
            }

        }

    }


    private void LargerAttackPowerup()
    {
        // Any additional effects of powerup if needed
    }

    private void LargerAttackRevert()
    {
        // Any additional effects of powerup if needed
    }



    private void increasedDamageEffect()
    {
        originalWeaponColor = playerWeaponRegularSpriteRend.color;
        originalDamageMod = playerWeaponRegularScript.damage;

        playerWeaponRegularSpriteRend.color = Color.red;
        playerWeaponRegularScript.damage = increasedDamageMod;

        playerWeaponLargerSpriteRend.color = Color.red;
        playerWeaponLargerScript.damage = increasedDamageMod;
    }

    private void increasedDamageRevert()
    {
        playerWeaponRegularSpriteRend.color = originalWeaponColor;
        playerWeaponRegularScript.damage = originalDamageMod;

        playerWeaponLargerSpriteRend.color = originalWeaponColor;
        playerWeaponLargerScript.damage = originalDamageMod;
    }



    private void speedUpEffect()
    {
        playerController.moveSpeedY *= speedUpMultiplier;
        playerController.moveSpeedX *= speedUpMultiplier;
    }
    private void speedUpRevert()
    {
        playerController.moveSpeedY /= speedUpMultiplier;
        playerController.moveSpeedX /= speedUpMultiplier;
    }



}
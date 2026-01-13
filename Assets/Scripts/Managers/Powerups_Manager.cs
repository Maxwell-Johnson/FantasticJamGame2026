using System;
using System.Collections;
using UnityEngine;

public class Powerups_Manager : MonoBehaviour
{

    public static Powerups_Manager Instance;

    public string powerupOne { get; private set; } = "Larger Attack Powerup";
    private float largerAttackDuration = 10f;
    private float largerAttackScaleMultiplier = 5f;
    private bool largerAttackPowerupIsActive;

    public string powerupTwo { get; private set; } = "Pickup 2";
    private float powerupTwoDuration = 10f;

    public string powerupThree { get; private set; } = "Pickup 3";
    private float powerupThreeDuration = 10f;

    private float powerupDuration;


    public Transform playerWeapon;
    private Vector3 originalScale;
    

    private void Awake()
    {
        Instance = this;
    }


    IEnumerator activatePowerupEnumerator(string powerupTag)
    {
        Debug.Log(powerupTag);
        if (powerupTag == powerupOne)
        {
            if (largerAttackPowerupIsActive)
            {
                // SET UP A CHECK SO THAT THE GAME DOESNT JUST MULTIPLY THE SCALES AGAIN AND KEEP MAKE IT BIGGER; MAKE IT ELONGATE POWERUP
            }
            else
            {
                powerupDuration = largerAttackDuration;
                LargerAttackPowerup();
                yield return new WaitForSeconds(powerupDuration);
                LargerAttackRevert();
            }
            
        }
        else if (powerupTag == powerupTwo)
        {
            powerupDuration = powerupTwoDuration;
            PowerupEffectTwo();
            yield return new WaitForSeconds(powerupDuration);
        }
        else if (powerupTag == powerupThree)
        {
            powerupDuration = powerupThreeDuration;
            PowerupEffectThree();
            yield return new WaitForSeconds(powerupDuration);

        }

    }

    public void activatePowerup(string powerupTag)
    {

        StartCoroutine(activatePowerupEnumerator(powerupTag));
    }


    private void LargerAttackPowerup()
    {
        Vector3 newScale = playerWeapon.localScale;
        originalScale = playerWeapon.localScale;

        newScale *= largerAttackScaleMultiplier;

        playerWeapon.localScale = newScale;

    }

    private void LargerAttackRevert()
    {
        playerWeapon.localScale = originalScale;
    }



    private void PowerupEffectTwo()
    {

    }

    private void PowerupRevertTwo()
    {

    }



    private void PowerupEffectThree()
    {
        
    }
    private void PowerupRevertThree()
    {

    }



}
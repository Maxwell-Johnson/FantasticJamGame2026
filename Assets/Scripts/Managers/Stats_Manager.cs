using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Game_Manager;

public class Stats_Manager : MonoBehaviour
{
    public static Stats_Manager Instance;

    //These variables keep track of the various stats across the game
    public int skullsCollected { get; private set; }
    public int enemiesDefeated { get; private set; }
    public int distanceTravelled { get; private set; }

    private void Awake()
    {
        Instance = this;

        //Subscribes to State Change function in Game Manager with the Reset Stats function; runs if state changes
        Game_Manager.OnGameStateChanged += ResetStats;
    }

    private void OnDestroy()
    {
        //Unsubscribes to prevent memeory leaks and errors
        Game_Manager.OnGameStateChanged -= ResetStats;
    }

    private void ResetStats(GameState gameState)
    {
        //If game has been reset, resets stats to 0
        if (gameState == GameState.GameReset)
        {
            skullsCollected = 0;
            enemiesDefeated = 0;
            distanceTravelled = 0;
        }
    }

    public void enemyDefeated()
    {
        enemiesDefeated++;
    }

    public void skullCollected(int value)
    {
        skullsCollected += value;
    }
}
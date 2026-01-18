using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Game_Manager;

public class Stats_Manager : MonoBehaviour
{
    public static Stats_Manager Instance;
    public Player_Leaderboard leaderboard;

    //These variables keep track of the various stats across the game
    public int skullsCollected { get; private set; }
    public int enemiesDefeated { get; private set; }
    public float distancesTravelled { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //Subscribes to State Change function in Game Manager with the Reset Stats function; runs if state changes
        Game_Manager.OnGameStateChanged += ResetStats;
        Game_Manager.OnGameStateChanged += SendFinalScore;

    }

    private void SendFinalScore(GameState state)
    {
        leaderboard.SendPlayerScore(skullsCollected);
    }

    private void OnDestroy()
    {
        //Unsubscribes to prevent memeory leaks and errors
        Game_Manager.OnGameStateChanged -= ResetStats;
        Game_Manager.OnGameStateChanged -= SendFinalScore;
    }

    private void ResetStats(GameState gameState)
    {
        //If game has been reset, resets stats to 0
        if (gameState == GameState.GameReset)
        {
            skullsCollected = 0;
            enemiesDefeated = 0;
            distancesTravelled = 0f;
        }
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        
    }

    public void SkullCollected(int value)
    {
        skullsCollected += value;
    }

    public void GrabCurrentScore(int currentScore)
    {
        currentScore = skullsCollected;
    }

    public void DistanceTravelled(float distanceTravelled_)
    {
        distancesTravelled += distanceTravelled_;
    }

    public float GrabCurrentDistanceTravelled ()
    {
         return distancesTravelled;
    }
}
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static Game_Manager;

public class Stats_Manager : MonoBehaviour
{
    public static Stats_Manager Instance;
    public Player_Leaderboard leaderboard;
    
    public int finalScore { get; private set; }

    //These variables keep track of the various stats across the game
    public int skullsCollected { get; private set; }
    public int enemiesDefeated { get; private set; }
    public float distancesTravelled { get; private set; }

    private float distanceTravelledTracker;


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

        leaderboard = GameObject.FindGameObjectWithTag("UI Canvas").GetComponentInChildren<Player_Leaderboard>();

    }

    private void Update()
    {
        CalculateScore();
    }
    private void SendFinalScore(GameState state)
    {
        if (state == GameState.PlayerDead)
        {
            leaderboard = GameObject.FindGameObjectWithTag("UI Canvas").GetComponentInChildren<Player_Leaderboard>();
            Debug.Log(finalScore);
            leaderboard.SendPlayerScore(finalScore);
        }
        
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
    // CHECK DEBUG DISTANCE TRAVELLED UPDATED???`
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

    public void CalculateScore()
    {

         float scoreMultiplier;

         scoreMultiplier = (distancesTravelled + 1000) / 1000;


         float skullScore = skullsCollected * 100;

         finalScore = (int)(skullScore * scoreMultiplier);
           


        
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static Stats_Manager;

public class Game_Manager : MonoBehaviour
{

    public static Game_Manager Instance;

    public GameState state;

    private int framerate = 60;

    public float leftSpawnerBound { get; private set; } = -4f;
    public float rightSpawnerBound { get; private set; } = 6f;

    //An event allows other scripts to subscribe to it and activate a function whenever the value is changed
    public static event Action<GameState> OnGameStateChanged;

    //Grabs the functionality from Input manager to read if someone presses the reset button
    InputAction restartAction;
    InputAction pauseAction;

    private Scene currentScene;

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        //Each of these cases will run depending on the gamestate
        switch (newState)
        {
            case GameState.GameRunning:
                break;
            case GameState.GamePaused:
                Time.timeScale = 0;
                break;
            case GameState.GameResumed:
                Time.timeScale = 1;
                UpdateGameState(GameState.GameRunning);
                break;
            case GameState.PlayerDead:
                Time.timeScale = 0;
                break;
            case GameState.GameReset:
                Time.timeScale = 1;
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }

    private void Awake()
    {
        Instance = this;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartAction = InputSystem.actions.FindAction("Restart");
        pauseAction = InputSystem.actions.FindAction("Pause");
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (restartAction.WasPressedThisFrame() && state != GameState.PlayerDead)
        {
            Debug.Log(state);
            currentScene = SceneManager.GetActiveScene();
            Debug.Log("RESTART");
            SceneManager.LoadScene(currentScene.name);
            UpdateGameState(GameState.GameReset);
            UpdateGameState(GameState.GameRunning);

        }

        if (pauseAction.WasPressedThisFrame())
        {
            if (state == GameState.GameRunning)
            {
                UpdateGameState(GameState.GamePaused);
            }
            else if (state == GameState.GamePaused)
            {
                UpdateGameState(GameState.GameResumed);
            }
        }
    }
}

public enum GameState
{
    GameRunning,
    GamePaused,
    GameResumed,
    PlayerDead,
    GameReset

}
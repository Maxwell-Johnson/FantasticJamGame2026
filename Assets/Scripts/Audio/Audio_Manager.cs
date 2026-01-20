
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager Instance;
    [Header("----- Auido Source -----")]
    
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("----- Audio Clip -----")]
    [SerializeField] public AudioClip mainTheme;
    [SerializeField] public AudioClip gameOverSong;
    [SerializeField] public AudioClip mainMenuTheme;
    [SerializeField] public AudioClip buttonClick;
    [SerializeField] public AudioClip owlAttack;
    [SerializeField] public AudioClip collectSkull;
    [SerializeField] public AudioClip wolfDeath;
    [SerializeField] public AudioClip playerDeath;
    [SerializeField] public AudioClip powerUpCollect;
    [SerializeField] public AudioClip wolfAttack;
    [SerializeField] public AudioClip playerHit;
    [SerializeField] public AudioClip owlFlap;
    [SerializeField] public AudioClip powerupEnd;
    [SerializeField] public AudioClip swingWeapon;
    [SerializeField] public AudioClip wolfSwim;
    [SerializeField] public AudioClip spiritAttack;
    [SerializeField] public AudioClip flowingWater;
    [SerializeField] public AudioClip owlDeath;
    [SerializeField] public AudioClip playerHitEnemy;
    [SerializeField] public AudioClip pause;
    [SerializeField] public AudioClip unpause;

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

        
    }

    private void OnDisable()
    {
        Game_Manager.OnGameStateChanged -= ConfigureMusic;
    }

    private void Start()
    {
        Game_Manager.OnGameStateChanged += ConfigureMusic;
        if (Game_Manager.Instance.state == GameState.MainMenu)
        {
            musicSource.clip = mainMenuTheme;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ConfigureMusic(GameState newState)
    {
        if (newState == GameState.PlayerDead)
        {
            musicSource.clip = gameOverSong;
            musicSource.Play();
        }
        else if (newState == GameState.GamePaused)
        {
            musicSource.Pause();
        }
        else if (newState == GameState.GameResumed)
        {
            musicSource.UnPause();
        }
        else if (newState == GameState.GameReset)
        {
            musicSource.clip = mainTheme;
            musicSource.Play();
        }
        else if (newState == GameState.MainMenu)
        {
            musicSource.clip = mainMenuTheme;
            musicSource.Play();
        }
        else if (newState == GameState.GameRunning)
        {
            musicSource.clip = mainTheme;
            musicSource.Play();
        }
    }
}

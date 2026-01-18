using TMPro;
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

        Game_Manager.OnGameStateChanged += ConfigureMusic;
    }

    private void Start()
    {
        musicSource.clip = mainTheme;
        musicSource.Play();
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
    }
}

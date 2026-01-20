using UnityEngine;

public class Animation_Handler_Wolf : MonoBehaviour
{

    public Animator anim;
    public GameObject hitboxCollider;


    private void Awake()
    {
        Game_Manager.OnGameStateChanged += PauseSound;
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        Game_Manager.OnGameStateChanged -= PauseSound;
        Destroy(hitboxCollider);
    }

    public void PlayAttackSound()
    {
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.wolfAttack);
    }

    private void PauseSound(GameState gameState)
    {
        if (gameState == GameState.GamePaused)
        {
            GetComponent<AudioSource>().Pause();
        }
        else if (gameState == GameState.GameResumed)
        {
            GetComponent<AudioSource>().UnPause();
        }
    }
}

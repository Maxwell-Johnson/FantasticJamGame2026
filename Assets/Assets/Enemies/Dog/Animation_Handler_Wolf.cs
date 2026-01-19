using UnityEngine;

public class Animation_Handler_Wolf : MonoBehaviour
{

    public Animator anim;
    public GameObject hitboxCollider;

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDestroy()
    {
        Destroy(hitboxCollider);
    }

    public void PlayAttackSound()
    {
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.wolfAttack);
    }
}

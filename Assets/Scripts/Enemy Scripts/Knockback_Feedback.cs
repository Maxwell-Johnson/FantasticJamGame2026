using System.Collections;
using UnityEngine;

public class Knockback_Feedback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    public float knockbackStrength = 2f;
    public float stunTime = 0.01f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Knockback(Transform weaponTransform)
    {
        Debug.Log("KNOCEKD");
        gameObject.GetComponent<Enemy_Movement>().takingKnockbackTrue();
        StartCoroutine(StunTimer());
        Vector2 direction = (transform.position - weaponTransform.position).normalized;
        rb.linearVelocity = direction * knockbackStrength;
    }

    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        gameObject.GetComponent<Enemy_Movement>().takingKnockbackFalse();

    }

}

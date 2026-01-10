using UnityEngine;

public class Enemy_Properties : MonoBehaviour
{
    private Rigidbody2D rb;
    private float maxHealth = 1f;
    private float currentHealth;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentHealth = maxHealth; //Sets current health to established max health
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject); //if health 0, dead
    }
}

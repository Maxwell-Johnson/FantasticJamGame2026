using UnityEngine;

public class Enemy_Properties : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float maxHealth = 2f;
    private float currentHealth;

    private Health playerHealth;

    private float attackDamage = 1f;

    private GameObject spawner;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Enemy Spawner 1");
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentHealth = maxHealth; //Sets current health to established max health
    }

    private void damagePlayer()
    {
        playerHealth.TakeDamage(attackDamage);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        spawner.GetComponent<Enemy_Spawner>().enemyList.Remove(gameObject);
        if (currentHealth <= 0) Destroy(gameObject); //if health 0, dead

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //If the thing we come in contact with has the HEALTH script, it will run the damage player function and damage them
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            damagePlayer();
        }
    }
}

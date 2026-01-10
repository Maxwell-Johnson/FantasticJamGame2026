using UnityEngine;

public class Enemy_Properties : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float maxHealth = 1f;
    private float currentHealth;

    private SpriteRenderer sprite;
    private bool isTakingDamage = false;
    private float damageTimer;
    private float damageInvulnerabilityTime = 0.5f;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        currentHealth = maxHealth; //Sets current health to established max health
    }

    public void TakeDamage(float damage)
    {
        isTakingDamage = true;
        currentHealth -= damage;
        if (currentHealth <= 0) Destroy(gameObject); //if health 0, dead
        while (isTakingDamage)
        {
            sprite.color = Color.black;
            sprite.color = Color.white;
        }

    }

    private void Update()
    {
        CheckTakingDamageTimer();
    }

    void CheckTakingDamageTimer()
    {

        
        if (isTakingDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInvulnerabilityTime)
            {
                damageTimer = 0;
                isTakingDamage = false;
                
            }
        }

    }
}

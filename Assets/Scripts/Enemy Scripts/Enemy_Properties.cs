using System.Collections;
using UnityEngine;

public class Enemy_Properties : MonoBehaviour
{
    private Rigidbody2D rb;
    private Knockback_Feedback knockbackScript;
    [SerializeField] public float meleeMaxHealth;
    [SerializeField] public float rangedMaxHealth;
    [SerializeField] public float soulMaxHealth;
    private float currentHealth;
    public bool rangedEnemy;
    public bool meleeEnemy;
    public bool soulEnemy;
    

    private Health playerHealth;


    private float attackDamage = 1f;

    private GameObject spawner;

    public bool enemyTookDamage { get; private set; } = false;
    public bool colliderTurnedOff;
    private float enemyInvulnerabilityTime = 0.5f;



    public GameObject skullPrefab;

    
    
    IEnumerator EnemyTookDamage(float enemyInvulnerability)
    {
        enemyTookDamage = true;
        yield return new WaitForSeconds(enemyInvulnerability);
        enemyTookDamage = false;
    }

    private void OnDestroy()
    {
        Game_Manager.OnGameStateChanged -= Destroy;
    }
    private void Awake()
    {

        Game_Manager.OnGameStateChanged += Destroy;

        if (rangedEnemy)
        {

            spawner = GameObject.FindGameObjectWithTag("Ranged Spawner");
        }
        else if (meleeEnemy)
        {
            spawner = GameObject.FindGameObjectWithTag("Enemy Spawner 1");
        }
        else if (soulEnemy)
        {
            spawner = GameObject.FindGameObjectWithTag("Soul Enemy Spawner");
        }

        rb = GetComponent<Rigidbody2D>();
        knockbackScript = GetComponent<Knockback_Feedback>();
    }
    void Start()
    {
        colliderTurnedOff = false;
        if (gameObject.CompareTag("Melee Enemy"))
        {
            currentHealth = meleeMaxHealth; //Sets current health to established max health
        }
        else if (gameObject.CompareTag("Ranged Enemy"))
        {
            currentHealth = rangedMaxHealth; //Sets current health to established max health
        }
        else if (gameObject.CompareTag("Soul Enemy"))
        {
            currentHealth = soulMaxHealth; //Sets current health to established max health
        }
    }

    private void damagePlayer()
    {


        playerHealth.TakeDamage(attackDamage);
        if (soulEnemy)
        {
            gameObject.GetComponent<Soul_Movement_Script>().colliderObject.SetActive(false);
            colliderTurnedOff = true;

        }
    }
    public void TakeDamage(float damage, Transform weaponTransform)
    {

        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.playerHitEnemy);
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            if (meleeEnemy)
            {
                Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.wolfDeath);
            }
            else if (rangedEnemy)
            {
                Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.owlDeath);
            }
            
            Die();
        }

        if (knockbackScript != null)
        {
            knockbackScript.Knockback(weaponTransform);
        }
        
        StartCoroutine(EnemyTookDamage(enemyInvulnerabilityTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //If the thing we come in contact with has the HEALTH script, it will run the damage player function and damage them
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            if (!playerHealth.playerTookDamage) damagePlayer();

        }
    }

    public void Die()
    {
        Vector3 deathPosition = gameObject.transform.position;
        Quaternion deathQuaternion = gameObject.transform.rotation;
        Pickups_Manager.Instance.SpawnSkullPickup(deathPosition, deathQuaternion);

        Stats_Manager.Instance.EnemyDefeated();

        RemoveFromList();
        Destroy(gameObject); //if health 0, dead
    }

    public void Destroy(GameState gameState)
    {
        if (gameState == GameState.PlayerDead || gameState == GameState.GameReset || gameState == GameState.MainMenu)
        {
            RemoveFromList();
            Destroy(gameObject); //if health 0, dead
        }
        
    }

    private void RemoveFromList()
    {
        if (meleeEnemy)
        {
            spawner.GetComponent<Enemy_Spawner>().enemyList.Remove(gameObject);
        }
        else if (rangedEnemy)
        {
            spawner.GetComponent<Enemy_Spawner>().rangeList.Remove(gameObject);
        }
        else if (soulEnemy)
        {
            spawner.GetComponent<Enemy_Spawner>().soulList.Remove(gameObject);
        }

    }
}

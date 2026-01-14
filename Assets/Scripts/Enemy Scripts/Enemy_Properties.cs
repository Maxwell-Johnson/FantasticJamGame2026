using System.Collections;
using UnityEngine;

public class Enemy_Properties : MonoBehaviour
{
    private Rigidbody2D rb;
    private Knockback_Feedback knockbackScript;
    [SerializeField] public float maxHealth = 2f;
    private float currentHealth;

    private Health playerHealth;

    private float attackDamage = 1f;

    private GameObject spawner;

    public bool enemyTookDamage { get; private set; } = false;
    private float enemyInvulnerabilityTime = 0.5f;



    public GameObject skullPrefab;

    IEnumerator EnemyTookDamage(float enemyInvulnerability)
    {
        enemyTookDamage = true;
        yield return new WaitForSeconds(enemyInvulnerability);
        enemyTookDamage = false;
    }

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Enemy Spawner 1");
        rb = GetComponent<Rigidbody2D>();
        knockbackScript = GetComponent<Knockback_Feedback>();
    }
    void Start()
    {
        currentHealth = maxHealth; //Sets current health to established max health
    }

    private void damagePlayer()
    {
        playerHealth.TakeDamage(attackDamage);
    }
    public void TakeDamage(float damage, Transform weaponTransform)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            
            Die();
        }

        knockbackScript.Knockback(weaponTransform);
        StartCoroutine(EnemyTookDamage(enemyInvulnerabilityTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //If the thing we come in contact with has the HEALTH script, it will run the damage player function and damage them
        if (collision.gameObject.GetComponent<Health>() != null)
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            if (!playerHealth.playerTookDamage) damagePlayer();
            else Debug.Log("Can't Take Damage");

        }
    }

    private void Die()
    {
        Vector3 deathPosition = gameObject.transform.position;
        Quaternion deathQuaternion = gameObject.transform.rotation;
        Pickups_Manager.Instance.SpawnSkullPickup(deathPosition, deathQuaternion);

        Stats_Manager.Instance.EnemyDefeated();

        RemoveFromList();
        Destroy(gameObject); //if health 0, dead
    }

    private void RemoveFromList()
    {
        spawner.GetComponent<Enemy_Spawner>().enemyList.Remove(gameObject);
    }
}

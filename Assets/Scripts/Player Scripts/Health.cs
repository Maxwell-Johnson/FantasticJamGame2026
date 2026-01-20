using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth = 3;
    public bool playerTookDamage { get; private set; }
    private float invulnerabilityTime = 1f;
    public float currentHealth { get; private set; } //allows other scripts to GET this value, but only this script can SET this value


    IEnumerator PlayerInvulnerable(float invulnerabilityTime)
    {
        playerTookDamage = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        playerTookDamage = false;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= -14)
        {
            PlayerDeath();
        }
    }
    public void TakeDamage(float damage)
    {

        
        if (currentHealth > 0)
        {

            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); //Clamp makes sure this value is never allowed to go below 0 when subtracing
            if (currentHealth == 0)
            {
                PlayerDeath();
            }
            else
            {
                Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.playerHit);
            }
            
        }
        StartCoroutine(PlayerInvulnerable(invulnerabilityTime));
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !playerTookDamage) TakeDamage(1); //If collide with obstacle, ouchie
    }

    private void PlayerDeath()
    {
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.playerDeath);
        Game_Manager.Instance.UpdateGameState(GameState.PlayerDead);
        Destroy(gameObject);
    }
}
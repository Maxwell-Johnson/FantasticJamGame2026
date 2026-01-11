using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth = 3;
    public float currentHealth { get; private set; } //allows other scripts to GET this value, but only this script can SET this value

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); //Clamp makes sure this value is never allowed to go below 0 when subtracing
            if (currentHealth == 0)
            {
                Game_Manager.Instance.UpdateGameState(GameState.PlayerDead);
                Debug.Log("I DEAD");
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle") TakeDamage(1); //If collide with obstacle, ouchie
    }
}
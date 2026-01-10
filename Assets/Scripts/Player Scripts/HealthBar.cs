using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameObject player;
    private Health playerHealth;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Health>();

        totalHealthBar.fillAmount = playerHealth.currentHealth / 10; //The bar uses number 0 - 1, so / 10 allows the health to equal the bar fill in this scenario

    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10; //Same here 
    }

}

using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_Properties enemy = collision.GetComponent<Enemy_Properties>(); //Grabs Enemy_Properties script from triggered object if it has it

        if (enemy != null) enemy.TakeDamage(damage, transform.parent); //If the triggered object does have the Enemy_Properties script, it will run their takedamage function
    }

}

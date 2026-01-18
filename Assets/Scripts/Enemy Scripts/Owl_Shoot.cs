using UnityEngine;

public class Owl_Shoot : MonoBehaviour
{

    
    private bool canAttack;
    private float attackCooldownTimer;
    private float attackCooldown = 3f;
    public GameObject projectilePrefab;
    public GameObject projectilesFolder;
    public GameObject owlParentObject;
    private Transform projectileSpawnLocation;
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        canAttack = false;
        
    }

    private void Update()
    {
        if (gameObject.GetComponent<Owl_Movement>().owlInPosition)
        {
            canAttack = true;
        }

        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;

            if (attackCooldownTimer <= 0)
            {
                canAttack = true;
            }
        }

        if (canAttack)
        {

            UpdateOwlTransform();
            ShootProjectile();
        }
    }

    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    private void ShootProjectile()
    {

        anim.SetBool("isAttacking", true);
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.owlAttack);
        Instantiate(projectilePrefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation);
        canAttack = false;
        attackCooldownTimer = attackCooldown;
    }

    private void UpdateOwlTransform()
    {
        projectileSpawnLocation = owlParentObject.transform;
    }
}

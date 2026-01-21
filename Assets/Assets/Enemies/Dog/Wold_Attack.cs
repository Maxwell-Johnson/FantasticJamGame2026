using System.Collections;
using UnityEngine;


public class Wold_Attack : MonoBehaviour
{

    public Animator anim;
    private Transform attackTarget;

    private void Start()
    {
        attackTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToSpot;

        if (attackTarget != null)
        {
            distanceToSpot = ((Vector2)attackTarget.transform.position - (Vector2)gameObject.transform.position).magnitude;

            if (distanceToSpot <= 5f)
            {
                anim.SetBool("isAttacking", true);
                
            }
        }
    }

    public void ActivateHitbox()
    {
        gameObject.GetComponentInChildren<PolygonCollider2D>().enabled = true;
        
    }

    public void DeActivateHitbox()
    {
        gameObject.GetComponentInChildren<PolygonCollider2D>().enabled = false;
    }
}


using UnityEngine;


public class Soul_Movement_Script : MonoBehaviour
{
    public Animator anim;

    private Rigidbody2D rb;
    private Transform followTarget;
    private float moveSpeed = 15f;
    private bool inWater;
    private float deadZone = -14f; 
    public GameObject colliderObject;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        followTarget = GameObject.FindGameObjectWithTag("Player").transform;
        inWater = true;

    }

    private void Update()
    {
        Swim();

        float distanceToSpot;

        if (followTarget != null)
        {
            distanceToSpot = ((Vector2)followTarget.transform.position - (Vector2)gameObject.transform.position).magnitude;

            if (distanceToSpot <= 3f && inWater)
            {
                PopOutOfWater();
            }
        }

        if (transform.position.y < deadZone) GetComponent<Enemy_Properties>().Die();

    }
    private void OnDestroy()
    {
        Destroy(colliderObject);
    }
    private void Swim()
    {
        rb.linearVelocity = new Vector2(0, -1) * moveSpeed;
    }

    private void PopOutOfWater()
    {
        inWater = false;
        anim.SetBool("playerInRange", true);
        Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.spiritAttack);
        if (gameObject.GetComponent<Enemy_Properties>().colliderTurnedOff == false)
        {
            colliderObject.SetActive(true);
        }
        
    }

    private void DonePopingOutOfWater()
    {
        
        
        anim.SetBool("outOfWater", true);
    }
    
}

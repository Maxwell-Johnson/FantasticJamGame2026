using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Controller : MonoBehaviour
{
    public InputActionAsset InputActions;

    #region COMPONENTS

    public Rigidbody2D rb { get; private set; }
    InputAction moveAction;
    InputAction attackAction;
    public Animator anim;

    #endregion

    #region MOVEMENT

    public float moveSpeedX = 5f;
    public float moveSpeedY = 7f;
    public Vector2 moveInput { get; private set; }

    private float playerPushSpeed;

    #endregion

    #region ATTACK

    [SerializeField] public GameObject attack;
    [SerializeField] public GameObject largerAttack;
    private float attackCooldown = 1f;
    private float attackTimer;

    #endregion

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable(); 
    }

    private void OnDisable()
    {
        //InputActions.FindActionMap("Player").Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackTimer = attackCooldown;
        rb = GetComponent<Rigidbody2D>(); //Gets the Rigidbody2D component from whatever this script is attached to.
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");

        
    }

    // Update is called once per frame
    void Update()
    {


        if (moveInput.y < 0)
        {
            playerPushSpeed = 0f;
        }
        else
        {
            playerPushSpeed = -2f;
        }
        moveInput = moveAction.ReadValue<Vector2>(); //Grabs the vector2 value from the Input Actions system "Move" and set it to moveInput
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeedX, (moveInput.y * moveSpeedY) + playerPushSpeed); //Changes the x and y velocity values using x and y of moveInput times movespeed

        attackTimer += Time.deltaTime;
        if (attackAction.WasPressedThisFrame() && attackTimer > attackCooldown)
        {
            AttackOn(attackCooldown);
            Audio_Manager.Instance.PlaySFX(Audio_Manager.Instance.swingWeapon);
        }

        
    }

    private void AttackOn(float attackCooldown)
    {
        attackTimer = 0;
        anim.SetBool("isAttacking", true);
        if (Powerups_Manager.Instance.largerAttackIsActive)
        {
            largerAttack.SetActive(true);

        }
        else
        {
            attack.SetActive(true);

        }


    }


    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
        if (Powerups_Manager.Instance.largerAttackIsActive)
        {

            largerAttack.SetActive(false);
        }
        else
        {

            attack.SetActive(false);
        }
    }
}

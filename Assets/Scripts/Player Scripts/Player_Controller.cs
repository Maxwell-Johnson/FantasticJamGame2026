using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public InputActionAsset InputActions;

    #region COMPONENTS

    private Rigidbody2D rb;
    InputAction moveAction;
    InputAction attackAction;

    #endregion

    #region MOVEMENT

    private float moveSpeedX = 3f;
    private float moveSpeedY = 7f;
    private Vector2 moveInput;

    private float playerDragSpeed = -2.4f;

    #endregion

    #region ATTACK

    [SerializeField] public GameObject attack;
    private bool isAttacking = false;
    private float attackDuration = 0.3f;
    private float attackTimer = 0f;

    #endregion

    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable(); 
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Gets the Rigidbody2D component from whatever this script is attached to.
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>(); //Grabs the vector2 value from the Input Actions system "Move" and set it to moveInput
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeedX, (moveInput.y * moveSpeedY) + playerDragSpeed); //Changes the x and y velocity values using x and y of moveInput times movespeed

        CheckAttackTimer(); //Checks the status of attack timer
        if (attackAction.WasPressedThisFrame())
        {
            Debug.Log("Attack!");
            Attack(); //Attacks when attack button is pressed
        }
    }

    void Attack()
    {
        //If player is not currently attacking, does attack.
        if (!isAttacking)
        {
            attack.SetActive(true);
            isAttacking = true;
            //Play Animation here
        }
    }

    void CheckAttackTimer()
    {

        //If player is attacking, adds current time (Deltatime) to the attack timer, and if the attack duration is less than timer, changes it back to false and resets timer.
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration)
            {
                attackTimer = 0;
                isAttacking = false;
                attack.SetActive(false);
            }
        }

    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public InputActionAsset InputActions;

    #region COMPONENTS

    private Rigidbody2D rb;
    InputAction moveAction;
    InputAction attackAction;

    #endregion

    #region MOVEMENT

    private float moveSpeedX = 4f;
    private float moveSpeedY = 6f;
    private Vector2 moveInput;

    private float playerPushSpeed;

    #endregion

    #region ATTACK

    [SerializeField] public GameObject attack;
    private bool isAttacking = false;
    private float attackDuration = 0.2f;
    private float attackCooldown = 0.8f;
    private bool canAttack;

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
        rb = GetComponent<Rigidbody2D>(); //Gets the Rigidbody2D component from whatever this script is attached to.
        moveAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        canAttack = true;
        
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

        if (attackAction.WasPressedThisFrame() && canAttack)
        {
            StartCoroutine(Attack(attackCooldown));
        }

        
    }


    IEnumerator Attack(float attackCooldown)
    {
        isAttacking = true;
        canAttack = false;
        attack.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attack.SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}

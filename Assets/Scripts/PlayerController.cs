using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.LightAnchor;

public class PlayerController : Character
{
    public float JumpHeight => jumpHeight;
    public bool IsGrounded => isGrounded;
    public Vector3 SnappedRotation => snappedRotation;
    public Weapon CurrentWeapon => currentWeapon;
    public bool WeaponEnabled => currentWeapon.gameObject.activeSelf;
    
    [Header("Movement Parameters")]
    [Range(0f, 1f)]
    [SerializeField] private float movementAcceleration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpCancelRate;
    [SerializeField] private float jumpPressWindow;
    [SerializeField] private float normalGravityScale;
    [SerializeField] private float fallingGravityScale;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Vector3 sphereCastStartOffset;
    [SerializeField] private LayerMask groundLayer;
    [Space]
    [Header("Current Movement State")]
    [SerializeField] private float movementInput;
    [SerializeField] private bool isGrounded;
    [Space]
    [Header("Weapons")]
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Weapon[] weapons;
    [Space]

    private Vector3 snappedRotation;

    private bool canTurn;
    private bool jumping;
    private float jumpPressTime;
    private float currentJumpCancelRate;

    private void Awake()
    {
        snappedRotation = transform.eulerAngles;
        canTurn = true;

        if (weapons.Length > 0 && weapons[0] != null) 
        {
            currentWeapon = weapons[0];
        }

        jumping = false;
        jumpPressTime = 0.0f;
        currentJumpCancelRate = 0.0f;
    }

    void Start()
    {
        InputManager.Instance.BindControls();
        InputManager.Instance.EnableAllControls();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DoDamage(100);
        }

        isGrounded = Physics.SphereCast(animator.transform.position + sphereCastStartOffset, 0.2f, Vector3.down, out _, groundCheckDistance, groundLayer);

        float eulerY = Mathf.Round(transform.eulerAngles.y);

        bool currentTurn = (movementInput < 0 && snappedRotation.y > 0) || (movementInput > 0 && snappedRotation.y < 0);
        bool previousTurn = eulerY == 90 || eulerY == 270;

        if (isGrounded)
        {
            currentJumpCancelRate = 0.0f;

            if (canTurn && currentTurn && previousTurn)
            {
                snappedRotation = Vector3.Scale(snappedRotation, new Vector3(1, -1, 1));
                animator.SetTrigger("Turn");

            }
        }

        if (jumping && jumpPressTime <= jumpPressWindow)
        {
            jumpPressTime += Time.deltaTime;
        }

        animator.SetFloat("HorizontalInput", Mathf.Abs(movementInput), 1 - movementAcceleration, Time.deltaTime);
        animator.SetFloat("HorizontalInputRaw", movementInput, 1 - movementAcceleration, Time.deltaTime);
    }
    void FixedUpdate()
    {
        Vector3 gravity;

        if (rb.velocity.y >= 0)
        {
            gravity = Physics.gravity.y * normalGravityScale * Vector3.up;
        }
        else
        {
            gravity = Physics.gravity.y * fallingGravityScale * Vector3.up;
        }

        rb.AddForce(gravity, ForceMode.Acceleration);
        rb.AddForce(Vector3.down * currentJumpCancelRate);
    }

    public void OnMovementButtonHeld(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<float>();
    }

    public void OnJumpButtonDown(InputAction.CallbackContext context)
    {
        if (!isGrounded)
        {
            return;
        }

        jumping = true;


        //For calculating how much force is required to reach a specific height
        float jumpForce = Mathf.Sqrt(JumpHeight * (Physics.gravity.y) * -2) * rb.mass;
        rb.AddForce(new Vector3(rb.velocity.x, jumpForce, 0), ForceMode.Impulse);

        animator.SetTrigger("Jump");
    }

    public void OnJumpButtonUp(InputAction.CallbackContext context)
    {
        if(jumpPressTime <= jumpPressWindow)
        {
            currentJumpCancelRate = jumpCancelRate;
        }

        jumping = false;
        jumpPressTime = 0.0f;
        animator.ResetTrigger("Jump");
    }

    public void OnAttackButtonDown(InputAction.CallbackContext context)
    {
        if(currentWeapon == null) {
            return;
        }

        canTurn = false;
        animator.SetBool("Attack", true);
    }

    public void OnAttackButtonUp(InputAction.CallbackContext context)
    {
        if (currentWeapon == null)
        {
            return;
        }

        canTurn = true;
        animator.SetBool("Attack", false);
    }

    protected override void Kill()
    {
        normalGravityScale = 0.0f;
        fallingGravityScale = 0.0f;
        capsuleCollider.enabled = false;
        animator.SetTrigger("Kill");
    }

    public void ToggleWeapon()
    {
        currentWeapon.gameObject.SetActive(!WeaponEnabled);
    }

    public override void DoDamage(float damage)
    {
        CurrentHealth -= damage;
    }

}

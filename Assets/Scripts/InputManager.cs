using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private InputAction attackAction;

    public void BindControls()
    {
        playerControls = new PlayerControls();

        moveAction = playerControls.Player.Move;
        moveAction.performed += playerController.OnMovementButtonHeld;

        jumpAction = playerControls.Player.Jump;
        jumpAction.started += playerController.OnJumpButtonDown;
        jumpAction.canceled += playerController.OnJumpButtonUp;

        attackAction = playerControls.Player.Attack;
        attackAction.started += playerController.OnAttackButtonDown;
        attackAction.canceled += playerController.OnAttackButtonUp;
    }

    public void UnbindControls()
    {
        playerControls = new PlayerControls();

        moveAction = playerControls.Player.Move;
        moveAction.performed -= playerController.OnMovementButtonHeld;

        jumpAction = playerControls.Player.Jump;
        jumpAction.performed -= playerController.OnJumpButtonDown;
        jumpAction.performed -= playerController.OnJumpButtonUp;

        attackAction = playerControls.Player.Attack;
        attackAction.started -= playerController.OnAttackButtonDown;
        attackAction.canceled -= playerController.OnAttackButtonUp;
    }

    public void EnableAllControls()
    {
        moveAction.Enable();
        jumpAction.Enable();
        attackAction.Enable();
    }
    public void DisableAllControls()
    {
        moveAction.Disable();
        jumpAction.Disable();
        attackAction.Disable();
    }
    private void OnEnable()
    {
        EnableAllControls();
    }
    private void OnDisable()
    {
        DisableAllControls();
    }
}

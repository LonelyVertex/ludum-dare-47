using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputState
{
    public Vector2 movement { get; private set; }
    public Vector2 mousePosition { get; private set; }

    public event Action fireEvent;
    public event Action dashEvent;

    private readonly PlayerInputActions _playerInputActions = new PlayerInputActions();
    
    private PlayerInputState()
    {
        _playerInputActions.Player.Move.performed += MovePerformed;
        _playerInputActions.Player.Move.canceled += MoveCancelled;
        _playerInputActions.Player.Look.performed += LookPerformed;

        _playerInputActions.Player.Dash.performed += DashPerformed;
        _playerInputActions.Player.Fire.started += FireStarted;
    }

    private void FireStarted(InputAction.CallbackContext ctx)
    {   
        fireEvent?.Invoke();
    }

    private void DashPerformed(InputAction.CallbackContext ctx)
    {
        dashEvent?.Invoke();
    }

    private void LookPerformed(InputAction.CallbackContext ctx)
    {
        mousePosition = ctx.ReadValue<Vector2>();
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();
    }

    private void MoveCancelled(InputAction.CallbackContext ctx)
    {
        movement = Vector2.zero;
    }

    public void EnableInput()
    {
        _playerInputActions.Enable();
    }

    public void DisableInput()
    {
        _playerInputActions.Disable();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerControl : InputSingleton<PlayerControl>
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlterateAction;

    private PlayerInputActions.PlayerActions playerActions;

    public Vector2 playerDirection => playerActions.Move.ReadValue<Vector2>();

    protected override void Awake()
    {
        base.Awake();
        playerActions = inputActions.Player;
        playerActions.Interact.performed += Interact_performed;
        playerActions.IneractAlternate.performed += InteractAlternate_performed;
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {   
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        OnInteractAlterateAction?.Invoke(this, EventArgs.Empty);
    }
}

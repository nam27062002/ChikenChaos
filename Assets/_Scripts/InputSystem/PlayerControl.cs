using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : InputSingleton<PlayerControl>
{
    private PlayerInputActions.PlayerActions playerActions;

    public Vector2 playerDirection => playerActions.Move.ReadValue<Vector2>();

    protected override void Awake()
    {
        base.Awake();
        playerActions = inputActions.Player;
    }

}

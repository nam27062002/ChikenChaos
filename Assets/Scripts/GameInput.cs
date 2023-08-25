using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        return inputActions.Player.Move.ReadValue<Vector2>();   
    }
}
 
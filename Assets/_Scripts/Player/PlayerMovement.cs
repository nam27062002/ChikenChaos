using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 0.2f;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private float playerSize = 0.5f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask counterMask;
    private Vector2 playerDirection;
    private Vector3 moveDir;
    private bool canMove;
    private Vector3 lastInteractDir;

    private void Update()
    {
        GetInput();
        HandleCollision();
        HandleInteract();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void GetInput()
    {
        playerDirection = PlayerControl.Instance.playerDirection;
        moveDir = new Vector3(playerDirection.x, 0f, playerDirection.y);
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
    }

    private void HandleMovement()
    {
        if (canMove && moveDir != Vector3.zero)
        {
            transform.position += moveDir * runSpeed;
            transform.forward = Vector3.Lerp(transform.forward, moveDir, moveSpeed);
        }
    }

    private void HandleCollision()
    {
        if (moveDir != Vector3.zero)
        {
            canMove = !CapsuleCast(moveDir);
            if (!canMove)
            {
                Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
                canMove = !CapsuleCast(moveDirX);
                if (canMove)
                {
                    moveDir = moveDirX;
                }
                else
                {
                    Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                    canMove = !CapsuleCast(moveDirZ);
                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    }
                }
            }
        }
    }

    private bool CapsuleCast(Vector3 direction)
    {
        return Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, direction, runSpeed);
    }
    
    private void HandleInteract()
    {
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance, counterMask))
        {
            if(hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }
}

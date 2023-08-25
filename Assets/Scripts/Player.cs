using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;
    private void Update()
    {

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x,0f, inputVector.y);

        float movementDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, movementDistance);
        if (!canmove) 
        {
            Vector3 movedirX = new Vector3(moveDir.x, 0, 0).normalized;
            canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movedirX, movementDistance);
            if(canmove)
            {
                moveDir = movedirX;
            }
            else
            {
                Vector3 movedirZ = new Vector3(0, 0, moveDir.z).normalized;
                canmove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movedirZ, movementDistance);
                if (canmove)
                {
                    moveDir = movedirZ;
                }
            }
        }
        if (canmove)
        {
            transform.position += moveDir * movementDistance;
        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
    public bool IsWalking => isWalking;
}

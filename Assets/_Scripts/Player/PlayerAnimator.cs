using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] private const string IS_WALKING = "IsWalking";
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAnimator.SetBool(IS_WALKING, false);
    }
    private void Update()
    {
        HandleAnimator();
    }
    private void HandleAnimator()
    {
        bool isWalking = PlayerControl.Instance.playerDirection != Vector2.zero;
        playerAnimator.SetBool(IS_WALKING, isWalking);
    }

}

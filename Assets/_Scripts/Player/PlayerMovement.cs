using System;
using _Scripts.Counter;
using _Scripts.Objects;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerMovement : MonoBehaviour,IKitchenObjectParent
    {
        private static PlayerMovement _instance;
        public static PlayerMovement Instance => _instance;
        public event EventHandler OnPickedSomething;
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
        public class OnSelectedCounterChangedEventArgs : EventArgs
        {
            public BaseCounter SelectedCounter;
        }

        [SerializeField] private float runSpeed = 0.2f;
        [SerializeField] private float moveSpeed = 0.3f;
        [SerializeField] private float playerSize = 0.5f;
        [SerializeField] private float playerHeight = 2f;
        [SerializeField] private float interactDistance = 2f;
        [SerializeField] private LayerMask counterMask;
        [SerializeField] private Transform kitchenObjectHoldPoint;

        private Vector2 playerDirection;
        private Vector3 moveDir;
        private bool canMove;
        private Vector3 lastInteractDir;
        private BaseCounter selectedCounter;
        private KitchenObject kitchenObject;
        public Vector3 MoveDir => moveDir;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        private void Start()
        {
            PlayerControl.Instance.OnInteractAction += GameInput_OnInteract;
            PlayerControl.Instance.OnInteractAlterateAction += GameInput_OnInteractAlterateAction;
        }

        private void GameInput_OnInteractAlterateAction(object sender, EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying()) return;
            if (selectedCounter != null)
            {
                selectedCounter.IntetRactAlternate(this);
            }
        }

        private void GameInput_OnInteract(object sender, EventArgs e)
        {
            if (!GameManager.Instance.IsGamePlaying()) return;
            if (selectedCounter != null)
            {
                selectedCounter.Interact(this);
            }

        }

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
            if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hitInfo, interactDistance, counterMask))
            {
                if (hitInfo.transform.TryGetComponent(out BaseCounter baseCounter))
                {
                    if (selectedCounter != baseCounter)
                    {
                        SetSelectedCounter(baseCounter);
                    }
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(BaseCounter counter)
        {
            selectedCounter = counter;
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                SelectedCounter = counter
            });
        }

        public void SetKitchenObject(KitchenObject o)
        {
            OnPickedSomething?.Invoke(this,EventArgs.Empty);
            this.kitchenObject = o;
        }
    
        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public Transform GetKitchenObjectFollowTranform()
        {
            return kitchenObjectHoldPoint;
        }

        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }

        public bool HasKitchenObject()
        {
            return kitchenObject != null;
        }
    }
}

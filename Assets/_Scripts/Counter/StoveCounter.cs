using System;
using _Scripts.Objects;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class StoveCounter : BaseCounter,IHasProgress
    {
        public event EventHandler<OnStateChangedEventAgrs> OnStateChanged;

        public class OnStateChangedEventAgrs : EventArgs
        {
            public State State;
        }

        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }
        
        [SerializeField] private FryingRecipeSO[] fryingRecipeSos;
        [SerializeField] private BurningRecipeSO[] burningRecipeSos;
        private FryingRecipeSO fryingRecipeSo;
        private BurningRecipeSO burningRecipeSo;
        private float fryingTimer;
        private float burningTimer;
        private State state;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChange;

        private void Start()
        {
            state = State.Idle;
        }

        private void Update()
        {
            if (!HasKitchenObject())
            {
                ResetStove();
                SetStateChange();
                SetTimerUI(int.MaxValue);
            }
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    HandleFrying();
                    break;
                case State.Fried:
                    HandleFried();
                    break;
                case State.Burned:
                    break;
            }
            
        }

        private void ResetStove()
        {
            state = State.Idle;
            burningTimer = 0;
            fryingTimer = 0;
        }
        private void HandleFrying()
        {
            fryingTimer += Time.deltaTime;
            SetTimerUI((float)fryingTimer / fryingRecipeSo.fryingTimermax);
            if (fryingTimer < fryingRecipeSo.fryingTimermax) return;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
            state = State.Fried;
            SetStateChange();
            burningTimer = 0f;
        }
        private void HandleFried()
        {
            if (burningRecipeSo == null)
            {
                burningRecipeSo = GetBurningRecipeSo(GetKitchenObject().KitchenObjectSo);
            }
            burningTimer += Time.deltaTime;
            SetTimerUI((float)burningTimer / burningRecipeSo.burningTimermax);
            if (burningTimer < fryingRecipeSo.fryingTimermax) return;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);
            state = State.Burned;
            burningTimer = 0f;
            SetStateChange();
        }
        public override void Interact(PlayerMovement player)
        {
            if (HasKitchenObject())
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                }
                else
                {
                    if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        if (plateKitchenObject != null)
                        {
                            if (plateKitchenObject.IsKitchenObjectExist(GetKitchenObject().KitchenObjectSo) || !plateKitchenObject.IsValidKitchenObject(GetKitchenObject().KitchenObjectSo)) return;
                            plateKitchenObject.AddIngredient(GetKitchenObject().KitchenObjectSo);
                            GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    fryingRecipeSo = GetFryingRecipeSo(player.GetKitchenObject().KitchenObjectSo);
                    if (fryingRecipeSo == null) return;
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    state = State.Frying;
                    fryingTimer = 0f;
                    SetStateChange();
                }
            }
        }
        
        private FryingRecipeSO GetFryingRecipeSo(KitchenObjectSO kitchenObjectSo)
        {
            foreach (FryingRecipeSO fryingRecipeSo in fryingRecipeSos)
            {
                if (fryingRecipeSo.input == kitchenObjectSo)
                {
                    return fryingRecipeSo;
                }
            }
            return null;
        }
        private BurningRecipeSO GetBurningRecipeSo(KitchenObjectSO kitchenObjectSo)
        {
            foreach (BurningRecipeSO burningRecipeSo in burningRecipeSos)
            {
                if (burningRecipeSo.input == kitchenObjectSo)
                {
                    return burningRecipeSo;
                }
            }
            return null;
        }

        private void SetStateChange()
        {
            OnStateChanged?.Invoke(this,new OnStateChangedEventAgrs()
            {
                State = state
            });
        }

        private void SetTimerUI(float progressNormalized)
        {
            OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
            {
                ProgressNormalized = progressNormalized
            });
        }

    }
}

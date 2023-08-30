using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class CuttingCounter : BaseCounter, IHasProgress
    {
        [SerializeField] private CuttingRecipeSO[] cuttingRecipeSoArray;
        private int cuttingProgress;
        private CuttingRecipeSO cutting;
        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChange;

        public event EventHandler OnCutting;
        public override void Interact(PlayerMovement player)
        {
            if (HasKitchenObject())
            {
                if (!player.HasKitchenObject())
                {
                    GetKitchenObject().SetKitchenObjectParent(player);
                    cuttingProgress = int.MaxValue;
                    SetOnProgressChange();

                }
            }
            else
            {
                if (player.HasKitchenObject())
                {
                    cutting = GetCuttingRecipeSo(player.GetKitchenObject().KitchenObjectSo);
                    if (cutting == null) return;
                    cuttingProgress = 0;
                    SetOnProgressChange();
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                
                }
            }
        }
        public override void IntetRactAlternate(PlayerMovement player)
        {
            if (!HasKitchenObject()) return;
            if (GetCuttingRecipeSo(GetKitchenObject().KitchenObjectSo) == null) return;
            cuttingProgress++;
            OnCutting?.Invoke(this, EventArgs.Empty);
            SetOnProgressChange();
            if (cuttingProgress < cutting.CuttingProcessMax) return;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutting.output, this);
        
        }

        private CuttingRecipeSO GetCuttingRecipeSo(KitchenObjectSO kitchenObjectSo)
        {
            foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSoArray)
            {
                if (cuttingRecipeSo.input == kitchenObjectSo)
                {
                    return cuttingRecipeSo;
                }
            }
            return null;
        }
        private void SetOnProgressChange()
        {
            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
            {
                ProgressNormalized = (float) cuttingProgress/cutting.CuttingProcessMax
            });
        }


    }
}

using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(PlayerMovement player)
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
    }
    public override void IntetRactAlternate(PlayerMovement player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO outputKitchenObjectSo = getOutPutKitchenObjectSo(GetKitchenObject().KitchenObjectSO);
            if (outputKitchenObjectSo == null) return;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSo, this);
        }
    }

    private KitchenObjectSO getOutPutKitchenObjectSo(KitchenObjectSO kitchenObjectSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == kitchenObjectSo)
            {
                return cuttingRecipeSo.output;
            }
        }
        return null;
    }
}

using _Scripts.Objects;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class ClearCounter : BaseCounter
    {
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
                    else
                    {
                        if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                        {
                            if (plateKitchenObject.IsKitchenObjectExist(player.GetKitchenObject().KitchenObjectSo) || !plateKitchenObject.IsValidKitchenObject(player.GetKitchenObject().KitchenObjectSo)) return;
                            plateKitchenObject.AddIngredient(player.GetKitchenObject().KitchenObjectSo);
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
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
    }
}

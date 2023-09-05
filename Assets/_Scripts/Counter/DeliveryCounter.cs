using System;
using _Scripts.Objects;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class DeliveryCounter : BaseCounter
    {
        private static DeliveryCounter instance;
        public static DeliveryCounter Instance => instance;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        public override void Interact(PlayerMovement player)
        {
            if(!player.HasKitchenObject()) return;
            if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) return;
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            player.GetKitchenObject().DestroySelf();
        }
    }
}

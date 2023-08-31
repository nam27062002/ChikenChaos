using System;
using _Scripts.Counter;
using _Scripts.Objects;
using _Scripts.Player;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;
    public override void Interact(PlayerMovement player)
    {
        if (player.HasKitchenObject()) return;
        Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}





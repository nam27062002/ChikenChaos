using System;
using _Scripts.Counter;
using _Scripts.Player;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrash;
    public override void Interact(PlayerMovement player)
    {
        if (!player.HasKitchenObject()) return;
        OnAnyObjectTrash?.Invoke(this,EventArgs.Empty);
        player.GetKitchenObject().DestroySelf();
    }
}


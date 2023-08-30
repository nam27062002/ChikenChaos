using _Scripts.Counter;
using _Scripts.Player;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerMovement player)
    {
        if (!player.HasKitchenObject()) return;
        
            player.GetKitchenObject().DestroySelf();
    }
}


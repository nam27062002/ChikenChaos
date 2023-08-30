using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Counter;
using _Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

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

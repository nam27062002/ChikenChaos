using System;
using _Scripts.Objects;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
    
        [SerializeField] private Transform counterTopPoint;
        public static event EventHandler OnDropedSomething;
        private KitchenObject kitchenObject;
    
        public virtual void Interact(PlayerMovement player) {}
    
        public virtual void IntetRactAlternate(PlayerMovement player) { }

        public void SetKitchenObject(KitchenObject o)
        {
            this.kitchenObject = o;
            OnDropedSomething?.Invoke(this,EventArgs.Empty);
        }

        public void ClearKitchenObject()
        {
            kitchenObject = null;
        }

        public Transform GetKitchenObjectFollowTranform()
        {
            return counterTopPoint;
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

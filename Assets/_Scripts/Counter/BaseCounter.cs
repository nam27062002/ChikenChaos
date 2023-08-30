using System;
using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Counter
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
    
        [SerializeField] private Transform counterTopPoint;

        private KitchenObject kitchenObject;
    
        public virtual void Interact(PlayerMovement player) {}
    
        public virtual void IntetRactAlternate(PlayerMovement player) { }

        public void SetKitchenObject(KitchenObject o)
        {
            this.kitchenObject = o;
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

using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Objects
{
    public class PlateKitchenObject : KitchenObject
    {
        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
        public class OnIngredientAddedEventArgs : EventArgs
        {
            public KitchenObjectSO KitchenObjectSo;
        }
        [SerializeField] private List<KitchenObjectSO> validKitchenObjectSos;
        private List<KitchenObjectSO> kitchenObjectSos;
        private void Awake()
        {
            kitchenObjectSos = new List<KitchenObjectSO>();
        }

        public bool IsKitchenObjectExist(KitchenObjectSO kitchenObjectSo)
        {
            return kitchenObjectSos.Contains(kitchenObjectSo);
        }

        public bool IsValidKitchenObject(KitchenObjectSO kitchenObjectSo)
        {
            return validKitchenObjectSos.Contains(kitchenObjectSo);
        }
          
        public void AddIngredient(KitchenObjectSO kitchenObjectSo)
        {
            OnIngredientAdded?.Invoke(this,new OnIngredientAddedEventArgs()
            {
                KitchenObjectSo = kitchenObjectSo
            });
            kitchenObjectSos.Add(kitchenObjectSo);
        }
    }
}

using System;
using System.Collections.Generic;
using _Scripts.Objects;
using UnityEngine;

namespace _Scripts.Animator.Counter
{
    public class PlateCompleteVisual : MonoBehaviour
    {
        [SerializeField] private PlateKitchenObject plateKitchenObject;
        [Serializable]
        public struct KitchenObjectSoGameObject
        {
            public KitchenObjectSO KitchenObjectSo;
            public GameObject GameObject;
        }
        
        [SerializeField] private List<KitchenObjectSoGameObject> kitchenObjectSoGameObjects;
        private void Start()
        {
            plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
            HideAllGameObject();
        }

        private void ShowGameObject(KitchenObjectSO kitchenObjectSo)
        {
            foreach (KitchenObjectSoGameObject kitchenObjectSoGameObject in kitchenObjectSoGameObjects)
            {
                if (kitchenObjectSoGameObject.KitchenObjectSo == kitchenObjectSo)
                {
                    kitchenObjectSoGameObject.GameObject.SetActive(true);
                    return;
                }
            }
        }
        private void HideAllGameObject()
        {
            foreach (KitchenObjectSoGameObject kitchenObjectSoGameObject in kitchenObjectSoGameObjects)
            {
                kitchenObjectSoGameObject.GameObject.SetActive(false);
            }
        }

        private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
        {
            ShowGameObject(e.KitchenObjectSo);
        }
    }
}

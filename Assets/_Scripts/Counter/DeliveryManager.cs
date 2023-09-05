using System;
using System.Collections.Generic;
using _Scripts.Objects;
using _Scripts.ObjectsSO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Counter
{
    public class DeliveryManager : MonoBehaviour
    {
        public event EventHandler<OnSpawnRecipeEventArgs> OnSpawnRecipe;
        public event EventHandler OnRecipeSuccess;
        public event EventHandler OnRecipeFailed;
        public class OnSpawnRecipeEventArgs : EventArgs
        {
            public List<RecipeSO> waitingRecipeSo;
        }
        
        private static DeliveryManager instance;
        public static DeliveryManager Instance => instance;
        [SerializeField] private RecipeListSO listSo;
        private List<RecipeSO> waitingRecipeSo;
        private float spawnRecipeTimer = 0f;
        private float spawnRecipeTimerMax = 4f;
        private int waitingRecipeMax = 4;
        private int successfulRecipesAmount;
        public int SuccessfulRecipesAmount => successfulRecipesAmount;
        private void Awake()
        {
            if (instance == null) instance = this;
            waitingRecipeSo = new List<RecipeSO>();
        }

        private void Update()
        {
            if (waitingRecipeSo.Count >= waitingRecipeMax) return;
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer > 0f) return;
            spawnRecipeTimer = spawnRecipeTimerMax;
            RecipeSO recipeSo = listSo.RecipeSos[Random.Range(0, listSo.RecipeSos.Count)];
            waitingRecipeSo.Add(recipeSo);
            SetOnSpawnRecipe();
            
        }
        
        private bool CompareKitchenObjectSo(List<KitchenObjectSO> so1, List<KitchenObjectSO> so2)
        {
            if (so1.Count != so2.Count) return false;
            foreach(KitchenObjectSO kitchenObjectSo in so1)
            {
                if (!so2.Contains(kitchenObjectSo)) return false;
            }
            foreach(KitchenObjectSO kitchenObjectSo in so2)
            {
                if (!so1.Contains(kitchenObjectSo)) return false;
            }
            return true;
        }
        public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
        {
            foreach (RecipeSO recipeSo in waitingRecipeSo)
            {
                if (CompareKitchenObjectSo(recipeSo.KitchenObjectSos, plateKitchenObject.KitchenObjectSos))
                {
                    Debug.Log($"NT - Remove {recipeSo.RecipeName}");
                    successfulRecipesAmount++;
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                    RemoveRecipeSo(recipeSo);
                    return true;
                }
            }
            OnRecipeFailed?.Invoke(this,EventArgs.Empty);
            return false;
        }

        public void RemoveRecipeSo(RecipeSO recipeSo)
        {
            waitingRecipeSo.Remove(recipeSo);
            SetOnSpawnRecipe();
        }
        private void SetOnSpawnRecipe()
        {
            OnSpawnRecipe?.Invoke(this,new OnSpawnRecipeEventArgs()
            {
                waitingRecipeSo = waitingRecipeSo
            });
        }
    }
}

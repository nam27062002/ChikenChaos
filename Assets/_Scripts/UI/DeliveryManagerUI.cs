using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Counter;
using _Scripts.ObjectsSO;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class DeliveryManagerUI : MonoBehaviour
{
    [FormerlySerializedAs("DeliveryManager")] [SerializeField] private DeliveryManager deliveryManager;
    [SerializeField] private Transform container;
    [FormerlySerializedAs("RecipeTemplates")] [SerializeField] private Transform recipeTemplates;
    private void Start()
    {
        deliveryManager.OnSpawnRecipe += DeliveryManagerOnOnSpawnRecipe;
        recipeTemplates.gameObject.SetActive(false);
    }

    private void ShowRecipe(List<RecipeSO> waitingRecipeSo)
    {
        foreach (Transform child in container)
        {
            if(child != recipeTemplates) Destroy(child.gameObject); 
        }
        foreach (RecipeSO recipeSo in waitingRecipeSo)
        {
            Transform recipeTemplate = Instantiate(recipeTemplates, container);
            recipeTemplate.gameObject.SetActive(true);
            DeliveryManagerSingleUI deliveryManagerSingleUI = recipeTemplate.GetComponent<DeliveryManagerSingleUI>();
            deliveryManagerSingleUI.SetText(recipeSo.RecipeName);
            deliveryManagerSingleUI.ShowIcon(recipeSo);
        }
    }
    private void DeliveryManagerOnOnSpawnRecipe(object sender, DeliveryManager.OnSpawnRecipeEventArgs e)
    {
        ShowRecipe(e.waitingRecipeSo);
    }
}

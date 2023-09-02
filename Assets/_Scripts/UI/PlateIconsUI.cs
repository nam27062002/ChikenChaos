using System;
using System.Collections.Generic;
using _Scripts.Objects;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateIconsUI : MonoBehaviour
{
    [FormerlySerializedAs("PlateKitchenObject")] [SerializeField] private PlateKitchenObject plateKitchenObject;
    [FormerlySerializedAs("IconTemplate")] [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        List<KitchenObjectSO> kitchenObjectSos = plateKitchenObject.KitchenObjectSos;
        foreach (KitchenObjectSO kitchenObjectSo in kitchenObjectSos)
        {
            Transform iconTransform = Instantiate(iconTemplate,transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSo(kitchenObjectSo);
        }
    }
}

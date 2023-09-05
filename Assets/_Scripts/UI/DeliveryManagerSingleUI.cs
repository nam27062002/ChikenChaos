using System;
using _Scripts.ObjectsSO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class DeliveryManagerSingleUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipeName;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private Transform ingredientImage;

        private void Awake()
        {
            ingredientImage.gameObject.SetActive(false);
        }

        public void ShowIcon(RecipeSO recipeSo)
        {
            foreach (Transform child in iconContainer)
            {
                if(child != ingredientImage) Destroy(child.gameObject);
            }
            foreach (KitchenObjectSO kitchenObjectSo in recipeSo.KitchenObjectSos)
            {
                
                Transform image = Instantiate(ingredientImage,iconContainer);
                image.gameObject.SetActive(true);
                image.GetComponent<Image>().sprite = kitchenObjectSo.sprite;
            }
        }
        public void SetText(string text)
        {
            recipeName.text = text;
        }
    }
}

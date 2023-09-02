using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class PlateIconsSingleUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        public void SetKitchenObjectSo(KitchenObjectSO kitchenObjectSo)
        {
            image.sprite = kitchenObjectSo.sprite;
        }
    }
}

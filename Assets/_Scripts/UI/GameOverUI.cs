using System;
using _Scripts.Counter;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI recipesDeliveredText;
        private void Awake()
        {
            Hide_ShowAlLChildObject(false);
        }

        private void Start()
        {
            GameManager.Instance.OnStateChanged += InstanceOnOnStateChanged;
        }

        private void InstanceOnOnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
        {
            if (e.State != GameManager.State.GameOver)
            {
                Hide_ShowAlLChildObject(false);
            }
            else
            {
                Hide_ShowAlLChildObject(true);
                recipesDeliveredText.text = DeliveryManager.Instance.SuccessfulRecipesAmount.ToString();
            }
        }

        private void Hide_ShowAlLChildObject(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }
    }
}

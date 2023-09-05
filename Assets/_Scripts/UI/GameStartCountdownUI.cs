using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class GameStartCountdownUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        private void Start()
        {
            Hide_ShowAlLChildObject(false);
            GameManager.Instance.OnStateChanged += InstanceOnOnStateChanged;
        }

        private void Update()
        {
            SetText(Math.Ceiling(GameManager.Instance.GetStartCountdown()).ToString(CultureInfo.InvariantCulture));
        }

        private void InstanceOnOnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
        {
            if (e.State != GameManager.State.CountdownToStart)
            {
                Hide_ShowAlLChildObject(false);
            }
            else
            {
                Hide_ShowAlLChildObject(true);
            }
            
        }

        public void SetText(string text)
        { 
            textMeshProUGUI.text = text;
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

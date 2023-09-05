using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class GamePlayingLockUI : MonoBehaviour
    {
        [SerializeField] private Image timer;
        private float gamePlayingTimer;
        private void Start()
        {
            gamePlayingTimer = GameManager.Instance.GamePlayingTimer;
            Hide_ShowAlLChildObject(false);
            GameManager.Instance.OnStateChanged += InstanceOnOnStateChanged;
        }

        private void Update()
        {
            SetLock(GameManager.Instance.GamePlayingTimer);
        }

        private void SetLock(float time)
        {
            timer.fillAmount =  (gamePlayingTimer - time) / gamePlayingTimer;
        }

        private void InstanceOnOnStateChanged(object sender, GameManager.OnStateChangedEventArgs e)
        {
            if (e.State != GameManager.State.GamePlaying)
            {
                Hide_ShowAlLChildObject(false);
            }
            else
            {
                Hide_ShowAlLChildObject(true);
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

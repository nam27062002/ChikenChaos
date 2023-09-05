using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class GamePauseUI : MonoBehaviour
    {
        [SerializeField] private Button buttonContinue;
        [SerializeField] private Button buttonMainMenu;
        private void Awake()
        {
            Hide_ShowAlLChildObject(false);
        }

        private void Start()
        {
            buttonContinue.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePauseGame();
            });
            buttonMainMenu.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
            GameManager.Instance.OnTogglePauseGame += InstanceOnOnTogglePauseGame;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnTogglePauseGame -= InstanceOnOnTogglePauseGame;
        }

        private void InstanceOnOnTogglePauseGame(object sender, EventArgs e)
        {
            Hide_ShowAlLChildObject(GameManager.Instance.IsGamePause);
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

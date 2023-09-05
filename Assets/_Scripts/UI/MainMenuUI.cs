using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;
        private void Awake()
        {
            playButton.onClick.AddListener(PlayClick);
            quitButton.onClick.AddListener(QuitCLick);
            Time.timeScale = 1f;
        }

        private void PlayClick()
        {
            Loader.Load(Loader.Scene.GameScene);
        }

        private void QuitCLick()
        {
            Application.Quit();
        }
    }
}

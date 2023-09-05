using System;
using UnityEngine;

namespace _Scripts
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        private bool isGamePause;
        public bool IsGamePause => isGamePause;
        public event EventHandler OnTogglePauseGame;
        public class OnStateChangedEventArgs : EventArgs
        {
            public State State;
        }

        public enum State
        {
            WaitingToStart,
            CountdownToStart,
            GamePlaying,
            GameOver,
        }

        private State state;
        private float waitingToStartTimer = 1f;
        private float countdownToStartTimer = 3f;
        private float gamePlayingTimer = 10f;
        public float GamePlayingTimer => gamePlayingTimer;
        private void Awake()
        {
            if (instance == null) instance = this;
            state = State.WaitingToStart;
        }

        private void Start()
        {
            isGamePause = false;
            PlayerControl.Instance.OnPauseAction += InstanceOnOnPauseAction;
        }

        private void InstanceOnOnPauseAction(object sender, EventArgs e)
        {
            TogglePauseGame();
        }

        private void Update()
        {
            switch (state)
            {
                case State.WaitingToStart:
                    waitingToStartTimer -= Time.deltaTime;
                    if (waitingToStartTimer > 0f) return;
                    state = State.CountdownToStart;
                    SetStateChanged(State.CountdownToStart);
                    break;
                case State.CountdownToStart:
                    countdownToStartTimer -= Time.deltaTime;
                    if (countdownToStartTimer > 0f) return;
                    state = State.GamePlaying;
                    SetStateChanged(State.GamePlaying);
                    break;
                case State.GamePlaying:
                    gamePlayingTimer -= Time.deltaTime;
                    if (gamePlayingTimer > 0f) return;
                    state = State.GameOver;
                    SetStateChanged(State.GameOver);
                    break;
                case State.GameOver:
                    break;
            }
            
        }

        private void SetStateChanged(State state)
        {
            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs()
            {
                State = state
            });
        }
        
        public bool IsGamePlaying()
        {
            return state == State.GamePlaying;
        }

        public float GetStartCountdown()
        {
            return countdownToStartTimer;
        }
        
        public void TogglePauseGame()
        {
            
            Time.timeScale = isGamePause ? 1f : 0f;
            isGamePause = !isGamePause;
            OnTogglePauseGame?.Invoke(this,EventArgs.Empty);
        }
    }
}

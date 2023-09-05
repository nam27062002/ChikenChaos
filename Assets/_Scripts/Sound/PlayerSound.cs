using _Scripts.Player;
using UnityEngine;

namespace _Scripts.Sound
{
    public class PlayerSound : MonoBehaviour
    {
        private float footstepTimer;
        private float flootstepTimerMax = 0.2f;
        private void Update()
        {
                if (PlayerMovement.Instance.MoveDir == Vector3.zero) return;
            footstepTimer += Time.deltaTime;
            if (footstepTimer < flootstepTimerMax) return;
            footstepTimer = 0f;
            SoundManager.Instance.PlaySoundMovement();
        }
    }
}

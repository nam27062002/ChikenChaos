using System;
using _Scripts.Counter;
using _Scripts.ObjectsSO;
using _Scripts.Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Sound
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance => instance;
        [SerializeField] private DeliveryManager deliveryManager;
        [SerializeField] private AudioClipRefsSO audioClipRefsSo;

        private void Awake()
        {
            if (instance == null) instance = this;
        }

        private void Start()
        {
            deliveryManager.OnRecipeSuccess += DeliveryManagerOnOnReicpeSuccess;
            deliveryManager.OnRecipeFailed += DeliveryManagerOnOnRecipeFailed;
            CuttingCounter.OnAnyCut += CuttingCounterOnOnCutting;
            PlayerMovement.Instance.OnPickedSomething += InstanceOnOnPickedSomething;
            BaseCounter.OnDropedSomething += BaseCounterOnOnDropedSomething;
            TrashCounter.OnAnyObjectTrash += TrashCounterOnOnAnyObjectTrash;
        }

        public  void PlaySoundMovement()
        {
            PlaySound(audioClipRefsSo.footstep,PlayerMovement.Instance.transform.position);
        }
        private void TrashCounterOnOnAnyObjectTrash(object sender, EventArgs e)
        {
            TrashCounter counter = sender as TrashCounter;
            if (counter == null) return;
            PlaySound(audioClipRefsSo.trash,counter.transform.position);
        }

        private void BaseCounterOnOnDropedSomething(object sender, EventArgs e)
        {
            BaseCounter counter = sender as BaseCounter;
            if (counter == null) return;
            PlaySound(audioClipRefsSo.objectDrop,counter.transform.position);
        }
        

        private void InstanceOnOnPickedSomething(object sender, EventArgs e)
        {
            PlaySound(audioClipRefsSo.objectPickup,PlayerMovement.Instance.transform.position);
        }
        private void CuttingCounterOnOnCutting(object sender, EventArgs e)
        {
            CuttingCounter cuttingCounter = sender as CuttingCounter;
            if (cuttingCounter == null) return;
            PlaySound(audioClipRefsSo.chop,cuttingCounter.transform.position);
        }

        private void DeliveryManagerOnOnRecipeFailed(object sender, EventArgs e)
        {
            PlaySound(audioClipRefsSo.deliveryFail,DeliveryCounter.Instance.transform.position);
        }

        private void DeliveryManagerOnOnReicpeSuccess(object sender, EventArgs e)
        {
            PlaySound(audioClipRefsSo.deliverySuccess,DeliveryCounter.Instance.transform.position);
        }

        private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
        {
            PlaySound(audioClips[Random.Range(0, audioClips.Length)],position,volume);
        }
        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip,position,volume);
        }
    }
}

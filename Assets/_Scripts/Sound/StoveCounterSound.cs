using _Scripts.Counter;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounterOnOnStateChanged;
    }

    private void StoveCounterOnOnStateChanged(object sender, StoveCounter.OnStateChangedEventAgrs e)
    {
        if (!(e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried)) audioSource.Pause();
        else audioSource.Play();
    }
}

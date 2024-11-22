using UnityEngine;
using UnityEngine.Rendering;
using static System.Collections.Specialized.BitVector32;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource radioSqueak;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayingBackground(bool isPlay)
    {
        PlayStop(backgroundMusic, isPlay);
    }

    public void PlayingRadio(bool isPlay)
    {
        PlayStop(radioSqueak, isPlay);
    }

    private void PlayStop(AudioSource sound, bool isPlay)
    {
        if (isPlay)
            sound.Play();
        else
            sound.Stop();
    }
}

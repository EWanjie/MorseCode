using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static System.Collections.Specialized.BitVector32;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound")]
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource radioSqueak;

    private bool isMusic;
    private bool isSound;

    public void SetAudio()
    {
        backgroundMusic.mute = AudioMixerManager.isMusic ? false : true;
        radioSqueak.mute = AudioMixerManager.isSound ? false : true;
    }

    private void Awake()
    {
        Instance = this;
        SetAudio();
    }

    public void PlayingRadio(bool isPlay)
    {
        if (isPlay)
            radioSqueak.Play();
        else
            radioSqueak.Stop();
    }
}

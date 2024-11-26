using UnityEngine;

public class AudioMixerManager : MonoBehaviour
{
    public static AudioMixerManager Instance { get; private set; }

    public static bool isMusic = true;
    public static bool isSound = true;
    public static string lastScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusic()
    {
        isMusic = !isMusic;
        AudioManager.Instance.SetAudio();
    }

    public void SetSound()
    {
        isSound = !isSound;
        AudioManager.Instance.SetAudio();
    }

}

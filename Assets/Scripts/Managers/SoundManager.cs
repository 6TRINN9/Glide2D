using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    [Header("Sound source")]
    public AudioSource SFXSource;
    public AudioSource musicSource;
    [Header("Sound prefabs")]
    public AudioClip soundUnlockLevel;
    public AudioClip soundRestartLevel;
    public AudioClip soundButton1;
    public AudioClip soundUIBack;

    public bool muteMusic = false;
    public bool muteSFX = false;

    private void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }
    private void Start ()
    {
        if (SaveLoadManager.Instance.SaveObject != null)
        {
            muteMusic = SaveLoadManager.Instance.SaveObject.muteMusic;
            musicSource.mute = muteMusic;
            muteSFX = SaveLoadManager.Instance.SaveObject.muteSFX;
            SFXSource.mute = muteSFX;
        }
    }

    public void PlayRestartLevelSound()
    {
        SFXSource.PlayOneShot(soundRestartLevel);
    }
    public void PlayUnlockLevelSound()
    {
        SFXSource.PlayOneShot(soundUnlockLevel);
    }

    public void TurnMuteMusic(bool value)
    {
        muteMusic = value;
        SaveLoadManager.Instance.SaveObject.muteMusic = muteMusic;
        musicSource.mute = muteMusic;
    }
    public void TurnMuteSFX(bool value)
    {
        muteSFX = value;
        SaveLoadManager.Instance.SaveObject.muteSFX = muteSFX;
        SFXSource.mute = muteSFX;
    }

    

    public void PlaySingle(AudioClip audio)
    {
        SFXSource.PlayOneShot(audio);
    }

}

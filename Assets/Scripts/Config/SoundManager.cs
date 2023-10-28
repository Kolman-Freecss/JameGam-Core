using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private static readonly string FirstPlay = "FirstPlay";
    public AudioClip buttonSound;
    public AudioClip[] backgroundMusic;
    
    [Range(0, 100)] public float EffectsAudioVolume = 50f;
    [Range(0, 100)] public float MusicAudioVolume = 40f;
    
    private int firstPlayInt;
    
    private static readonly string MusicAudioPref = "MusicAudioPref";
    private static readonly string EffectsAudioPref = "EffectsAudioPref";
    
    private AudioSource _backgroundAudio;

    #region InitData

    private void Awake()
    {
        ManageSingleton();
    }
    
    void Start()
    {
        GetReferences();
        //Para incluir el event system
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (firstPlayInt == 0)
        {
            MusicAudioVolume = .125f;
            EffectsAudioVolume = .75f;
            SetMusicVolume(MusicAudioVolume);
            SetEffectsVolume(EffectsAudioVolume);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            MusicAudioVolume = PlayerPrefs.GetFloat(MusicAudioPref);
            EffectsAudioVolume = PlayerPrefs.GetFloat(EffectsAudioPref);
        }
        StartBackgroundMusic(0);
    }
    
    private void GetReferences()
    {
        _backgroundAudio = GetComponent<AudioSource>();
    }

    void ManageSingleton()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public void StartBackgroundMusic(int clip)
    {
        _backgroundAudio.clip = backgroundMusic[clip];
        _backgroundAudio.Play();
    }
    
    public void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat(EffectsAudioPref, volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicAudioPref, volume);
        this.MusicAudioVolume = volume;
    }
    
    public float GetSoundVolume()
    {
        return EffectsAudioVolume;
    }
    
    public float GetMusicVolume()
    {
        return MusicAudioVolume;
    }
    
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicAudioPref, MusicAudioVolume);
        PlayerPrefs.SetFloat(EffectsAudioPref, EffectsAudioVolume);
    }

    public void UpdateSound(float musicVolume, float soundEffectsVolume)
    {
        this.MusicAudioVolume = musicVolume;
        this.EffectsAudioVolume = soundEffectsVolume;
    }

    public void PlayButtonClickSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(buttonSound, transform.position);
    }
}
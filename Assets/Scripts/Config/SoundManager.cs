using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private static readonly string FirstPlay = "FirstPlay";
    [FormerlySerializedAs("buttonAudioSource")] public AudioClip buttonClip;
    public AudioClip[] backgroundMusic;
    
    [Range(0, 1)] public float EffectsAudioVolume = .5f;
    [Range(0, 1)] public float MusicAudioVolume = .4f;
    
    private int firstPlayInt;
    
    private static readonly string MusicAudioPref = "MusicAudioPref";
    private static readonly string EffectsAudioPref = "EffectsAudioPref";
    
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource buttonAudioSource;

    #region InitData

    private void Awake()
    {
        ManageSingleton();
    }
    
    void Start()
    {
        Debug.Log("Loading Sounds...");
        GetReferences();
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if (firstPlayInt == 0)
        {
            SetMusicVolume(.5f);
            SetEffectsVolume(.5f);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            SetMusicVolume(PlayerPrefs.GetFloat(MusicAudioPref));
            SetEffectsVolume(PlayerPrefs.GetFloat(EffectsAudioPref));
        }
        StartBackgroundMusic(0);
    }
    
    private void GetReferences()
    {
        buttonAudioSource.clip = buttonClip;
        //backgroundAudio = GetComponent<AudioSource>();
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
        backgroundAudio.clip = backgroundMusic[clip];
        backgroundAudio.Play();
    }

    #region Getters & Setters

    public void SetEffectsVolume(float volume)
    {
        PlayerPrefs.SetFloat(EffectsAudioPref, volume);
        EffectsAudioVolume = volume;
        buttonAudioSource.volume = volume;
    }
    
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MusicAudioPref, volume);
        MusicAudioVolume = volume;
        backgroundAudio.volume = volume;
    }
    
    public float GetSoundVolume()
    {
        return EffectsAudioVolume;
    }
    
    public float GetMusicVolume()
    {
        return MusicAudioVolume;
    }

    #endregion
    
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicAudioPref, MusicAudioVolume);
        PlayerPrefs.SetFloat(EffectsAudioPref, EffectsAudioVolume);
    }

    public void PlayButtonClickSound(Vector3 position)
    {
        Debug.Log("PlayButtonClickSound"+EffectsAudioVolume);
        buttonAudioSource.Play();
        //AudioSource.PlayClipAtPoint(buttonClip, transform.position, EffectsAudioVolume);
    }
}
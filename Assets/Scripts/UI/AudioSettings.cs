using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, soundEffectsSlider;

    public GameObject buttonFirstSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonFirstSelected);

        musicSlider.value = SoundManager.Instance.MusicAudioVolume;
        soundEffectsSlider.value = SoundManager.Instance.EffectsAudioVolume;
    }

    public void SaveSoundSettings()
    {
        SoundManager.Instance.SaveSoundSettings();
    }

    public void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateEffectsSound()
    {
        SoundManager.Instance.SetEffectsVolume(soundEffectsSlider.value);
    }

    public void UpdateMusicSound()
    {
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
    }
}
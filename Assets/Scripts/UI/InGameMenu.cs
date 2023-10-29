using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private Slider musicSlider, soundEffectsSlider;
    [SerializeField] private Slider sliderBrigthness;
    [SerializeField]
    private Light sceneLight; 
    [SerializeField] private Toggle windowedToggle, bloodToggle;
    
    #region InitData

    void Start()
    {
        Init();
        //PlayerBehaviour.Instance.Inputs.OnEscapeTrigger += OnPressEscape;
    }

    void Init()
    {
        inGameMenu.SetActive(false);
        musicSlider.value = SoundManager.Instance.MusicAudioVolume;
        soundEffectsSlider.value = SoundManager.Instance.EffectsAudioVolume;
        sliderBrigthness.value = sceneLight.intensity;
        windowedToggle.isOn = DisplaySettings.Instance.windowed;
        bloodToggle.isOn = DisplaySettings.Instance.blood;
    }

    #endregion

    //void OnPressEscape(bool pressed)
    //{
    //    if (PlayerBehaviour.Instance.Inputs.escape)
    //    {
    //        PauseGame(GameManager.Instance.IsPaused);
    //    }
    //}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerBehaviour.Instance.Inputs.escape)
            {
                PauseGame(GameManager.Instance.IsPaused);
            }
        }
    }

    public void PauseGame(bool paused)
    {
        GameManager.Instance.PauseGameEvent(paused);
        inGameMenu.SetActive(GameManager.Instance.IsPaused);
    }
    
    public void OnExitMenu()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        GameManager.Instance.ExitGameSession();
    }

    public void OnUpdateBrightness()
    {
        sceneLight.intensity = sliderBrigthness.value;
    }
    
    public void UpdateEffectsSound()
    {
        SoundManager.Instance.SetEffectsVolume(soundEffectsSlider.value);
    }

    public void UpdateMusicSound()
    {
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
    }
    
    public void OnToogleBlood()
    {
        DisplaySettings.Instance.ToggleBlood(bloodToggle.isOn);
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
    }
    
    public void OnToggleWindowed()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        DisplaySettings.Instance.ToggleWindowed(windowedToggle.isOn);
    }
    
}
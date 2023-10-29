using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider sliderBrigthness;
    [SerializeField]
    private Light sceneLight; //puede que luego se tenga que modificar en funcion de la ilu de la escena
    [SerializeField] private Toggle windowedToggle, bloodToggle;
    
    public void ToggleWindowed()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        DisplaySettings.Instance.windowed = windowedToggle.isOn;
        Screen.fullScreen = DisplaySettings.Instance.windowed;
    }

    public void UpdateBrightness()
    {
        sceneLight.intensity = sliderBrigthness.value;
    }

    public void ToogleBlood()
    {
        DisplaySettings.Instance.ToggleBlood(bloodToggle.isOn);
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
    }
}

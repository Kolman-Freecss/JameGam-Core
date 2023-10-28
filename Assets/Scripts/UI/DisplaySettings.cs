using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{
    public static DisplaySettings Instance { get; private set; }

    [SerializeField] private Slider sliderBrigthness;

    [SerializeField]
    private Light sceneLight; //puede que luego se tenga que modificar en funcion de la ilu de la escena

    [SerializeField] private GameObject toggle;
    public bool blood = true;
    public bool windowed = true;

    #region InitData

    private void Awake()
    {
        ManageSingleton();
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

    public void ToggleWindowed()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void UpdateBrightness()
    {
        sceneLight.intensity = sliderBrigthness.value;
    }

    public void ToogleBlood()
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
    }
}
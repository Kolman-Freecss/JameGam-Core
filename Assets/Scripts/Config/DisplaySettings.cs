using System;
using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    public static DisplaySettings Instance { get; private set; }

    public bool blood = true;
    public bool windowed = true;
    
    public event Action<bool> OnBloodToogle; 

    #region InitData

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    #endregion

    public void ToggleBlood(bool value)
    {
        blood = value;
        OnBloodToogle?.Invoke(blood);
    }
    
    public void ToggleWindowed(bool value)
    {
        SoundManager.Instance.PlayButtonClickSound(Camera.main.transform.position);
        windowed = value;
        Screen.fullScreen = windowed;
    }
    
}
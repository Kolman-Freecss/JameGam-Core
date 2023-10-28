using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{
    [SerializeField] private Slider sliderBrigthness;
    [SerializeField] private Light sceneLight; //puede que luego se tenga que modificar en funcion de la ilu de la escena

    [SerializeField] private GameObject toggle;

    public void ToggleWindowed()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void UpdateBrightness()
    {
        sceneLight.intensity = sliderBrigthness.value;
    }
}

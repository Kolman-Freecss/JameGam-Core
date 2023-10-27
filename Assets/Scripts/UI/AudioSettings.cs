using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]private Slider musicSlider, sfxSlider;
  //  [SerializeField]private GameObject bloodToggle;
    [SerializeField] private AudioSource backgroundSource;
    public GameObject buttonFirstSelected;

    // Start is called before the first frame update
    void Start()
    {
        //Para incluir el event system
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonFirstSelected);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveSoundSettings()
    {

    }
    public void UpdateSound()
    {

    }
}

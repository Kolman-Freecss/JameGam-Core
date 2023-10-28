using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;

    public void PlayEffect ()
    {
        buttonSound.Play();
    }
}

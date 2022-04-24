using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlBackgroundAudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    public Slider audioSlider;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void MuteAudio()
    {
            audioSource.mute = !audioSource.mute;
    }

    

    //Invoked when a submit button is clicked.
    public void SliderAudioAdjust()
    {
        audioSource.volume = audioSlider.value;
    }
}

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
        if (PlayerPrefs.GetFloat("antivolume") == 0)
            audioSource.volume = 0.3f ;
        else
            audioSource.volume = 1f - PlayerPrefs.GetFloat("antivolume");
        audioSlider.value = audioSource.volume;
    }

    public void MuteAudio()
    {
            audioSource.mute = !audioSource.mute;
    }

    

    //Invoked when a submit button is clicked.
    public void SliderAudioAdjust()
    {
        audioSource.volume = audioSlider.value;
        PlayerPrefs.SetFloat("antivolume", 1f - audioSource.volume);
        PlayerPrefs.Save();
    }
}

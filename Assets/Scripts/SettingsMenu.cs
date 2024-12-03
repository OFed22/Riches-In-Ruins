using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour{
  
    [SerializeField] private Slider volumeSlider; // Drag your slider here in the Inspector
    [SerializeField] private AudioSource audioSource; // Drag your audio source here in the Inspector

    private void Start()
    {
        // Initialize the slider value to match the audio source volume
        if (audioSource != null)
        {
            volumeSlider.value = audioSource.volume;
        }

        // Add a listener to call the OnVolumeChange method whenever the slider value changes
        volumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }

    // This method is called whenever the slider value changes
    private void OnVolumeChange(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value; // Set the audio source volume to the slider value
        }
    }

    public void SetFullscreen(bool isFullscreen){
        Screen.fullScreen = isFullscreen;
    }
}

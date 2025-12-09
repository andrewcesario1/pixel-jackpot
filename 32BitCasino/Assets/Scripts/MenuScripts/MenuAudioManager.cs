using UnityEngine;
using UnityEngine.UI;

public class MenuAudioManager : MonoBehaviour
{
    public AudioSource menuMusicSource; // Assign the menu music AudioSource in the Inspector
    public Slider menuMusicSlider; // Assign the menu music volume slider in the Inspector

    private void Start()
    {
        // Initialize volume from PlayerPrefs or set default
        float savedVolume = PlayerPrefs.GetFloat("menuMusicVolume", 1f);
        menuMusicSource.volume = savedVolume;
        menuMusicSlider.value = savedVolume;

        // Add listener for slider value change
        menuMusicSlider.onValueChanged.AddListener(SetMenuMusicVolume);

        // Start playing menu music if not already playing
        if (!menuMusicSource.isPlaying)
        {
            menuMusicSource.Play();
        }
    }

    private void SetMenuMusicVolume(float volume)
    {
        menuMusicSource.volume = volume;
        PlayerPrefs.SetFloat("menuMusicVolume", volume);
        PlayerPrefs.Save();
    }
}

using UnityEngine;
using UnityEngine.UI;

public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance;

    public AudioSource gameMusicSource; // Assign the game music AudioSource in the Inspector
    public AudioSource soundEffectSource; // Assign the sound effects AudioSource in the Inspector
    public Slider gameMusicSlider; // Assign the game music volume slider in the Inspector
    public Slider soundEffectSlider; // Assign the sound effects volume slider in the Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicates
        }
    }

    private void Start()
    {
        // Initialize volumes from PlayerPrefs or set defaults
        float savedGameMusicVolume = PlayerPrefs.GetFloat("gameMusicVolume", 1f);
        float savedSoundEffectVolume = PlayerPrefs.GetFloat("soundEffectVolume", 1f);
        gameMusicSource.volume = savedGameMusicVolume;
        soundEffectSource.volume = savedSoundEffectVolume;

        gameMusicSlider.value = savedGameMusicVolume;
        soundEffectSlider.value = savedSoundEffectVolume;

        // Add listeners for slider value changes
        gameMusicSlider.onValueChanged.AddListener(SetGameMusicVolume);
        soundEffectSlider.onValueChanged.AddListener(SetSoundEffectVolume);

        // Start playing game music if not already playing
        if (!gameMusicSource.isPlaying)
        {
            gameMusicSource.Play();
        }
    }

    private void SetGameMusicVolume(float volume)
    {
        gameMusicSource.volume = volume;
        PlayerPrefs.SetFloat("gameMusicVolume", volume);
        PlayerPrefs.Save();
    }

    private void SetSoundEffectVolume(float volume)
    {
        soundEffectSource.volume = volume;
        PlayerPrefs.SetFloat("soundEffectVolume", volume);
        PlayerPrefs.Save();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (soundEffectSource != null && clip != null)
        {
            soundEffectSource.PlayOneShot(clip);
        }
        else
        {
            if (soundEffectSource == null)
                Debug.LogWarning("SoundEffectSource is not assigned in GameAudioManager.");
            if (clip == null)
                Debug.LogWarning("No AudioClip provided for sound effect.");
        }
    }

}

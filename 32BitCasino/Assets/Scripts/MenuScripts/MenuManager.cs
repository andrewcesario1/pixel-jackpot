using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Import SceneManagement for scene navigation

public class MenuManager : MonoBehaviour
{
    public Button playBtn; // Reference to the Play button

    void Start()
    {
        string currentVersion = "4.2"; // Update this version number with each new build

        // Check if the saved version key matches the current version
        if (PlayerPrefs.GetString("GameVersion", "") != currentVersion)
        {
            PlayerPrefs.DeleteAll(); // Clear all previous saved data
            PlayerPrefs.SetString("GameVersion", currentVersion); // Save the new version key
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs cleared for new version: " + currentVersion);
        }

        playBtn.onClick.AddListener(LoadGameScreen);
    }

    void LoadGameScreen()
    {
        SceneManager.LoadScene("gameScene");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();        
    }
}

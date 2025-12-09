using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // The Pause Menu UI Panel
    public GameObject gameInfoPanel;
    public PlayerController player; // Reference to the playerController for saving the game
    public bool isPaused = false;   // Track whether the game is paused

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();  // If the game is paused, unpause it
            }
            else
            {
                PauseGame();   // If the game is not paused, pause it
            }
        }
    }

    // Method to pause the game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Show the pause menu
        Time.timeScale = 0f;          // Freeze the game by setting time scale to 0
        isPaused = true;
    }

    // Method to resume the game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f;          // Unfreeze the game by setting time scale back to 1
        isPaused = false;
    }

    public void OpenGameInfo()
    {
        gameInfoPanel.SetActive(true); // Hide the pause menu
    }

    public void CloseGameInfo()
    {
        gameInfoPanel.SetActive(false); // Hide the pause menu
    }

    // Method to save the game
    public void SaveGame()
    {
        player.SaveData();           // Save the player's money using the playerController script
        Debug.Log("Game saved!");
        
        // Load the Main Menu scene
        Time.timeScale = 1f;          // Unfreeze time before switching scenes
        SceneManager.LoadScene("Main Menu"); 
    }

    // Method to load the main menu
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;          // Unfreeze time before switching scenes
        SceneManager.LoadScene("Main Menu");
    }

    // Method to open the settings menu
    public void OpenSettings()
    {
        Debug.Log("Opening settings...");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}

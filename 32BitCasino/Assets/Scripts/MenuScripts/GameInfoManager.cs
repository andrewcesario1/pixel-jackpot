using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoManager : MonoBehaviour
{
    public GameObject gameInfoPanel; // Reference to the panel that displays the rules
    public Button gameInfoButton; // Button to show the rules panel
    public Button closeButton; // Button to close the rules panel

    void Start()
    {
        // Hide the rules panel initially
        gameInfoPanel.SetActive(false);

        // Add listeners to the buttons
        gameInfoButton.onClick.AddListener(ShowRules);
        closeButton.onClick.AddListener(CloseRules);
    }

    // Show the rules panel
    void ShowRules()
    {
        gameInfoPanel.SetActive(true);
    }

    // Hide the rules panel
    void CloseRules()
    {
        gameInfoPanel.SetActive(false);
    }
}

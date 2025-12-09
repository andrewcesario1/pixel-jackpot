using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel; // Reference to the panel that displays the rules
    public Button settingsButton; // Button to show the rules panel
    public Button closeButton; // Button to close the rules panel

    void Start()
    {
        // Hide the rules panel initially

        // Add listeners to the buttons
        settingsButton.onClick.AddListener(ShowSettings);
        closeButton.onClick.AddListener(CloseRules);
    }

    // Show the rules panel
    void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    // Hide the rules panel
    void CloseRules()
    {
        settingsPanel.SetActive(false);
    }
}

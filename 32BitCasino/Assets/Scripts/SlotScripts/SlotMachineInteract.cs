using TMPro;
using UnityEngine;

public class SlotMachine : Interactable
{
    public GameObject slotMachineUI; // The slot machine UI panel
    public GameObject slotInfoPanel; // The slot machine UI panel
    protected bool isPlayerInRange = false; // Track if player is colliding with the slot machine
    public GameObject interactMsg;
    public ReelManager reelManager; // Reference to the ReelManager to control the spinning
    public GameObject tutorialMsg;
    public GameObject costText;
    

    void Start()
    {
        interactMsg.SetActive(false);
        costText.SetActive(false);
    }
    // Override the Interact method to define what happens when the player interacts
    public override void Interact()
    {
        Debug.Log("Player is interacting with the slot machine.");
        interactMsg.SetActive(false);
        // Open the slot machine UI
        slotMachineUI.SetActive(true);
        costText.SetActive(true);
        // Optionally, disable player movement, etc.
        FindObjectOfType<PlayerController>().canMove = false; // Disable player movement
    }

    private void Update()
    {
        // Check if player is in range and presses Space
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    // When the player enters the collider, set the flag to true
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player is in range of the slot machine.");
        interactMsg.SetActive(true);
        isPlayerInRange = true;
    }

    // When the player exits the collider, set the flag to false
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Player left the slot machine area.");
        interactMsg.SetActive(false);
        isPlayerInRange = false;
    }

    // Trigger spin when the button is clicked
    public void OnSpinButtonClick()
    {
        tutorialMsg.SetActive(false);
        Debug.Log("Player clicked Spin button.");
        reelManager.StartSpin(); // Trigger reel spin via ReelManager
    }

    public void OpenSlotInfo()
    {
        slotInfoPanel.SetActive(true); // Hide the slot machine UI panel
    }

    public void CloseSlotInfo()
    {
        slotInfoPanel.SetActive(false); // Hide the slot machine UI panel
    }

    public void CloseSlotMachineUI()
    {
        slotMachineUI.SetActive(false); // Hide the slot machine UI panel
        interactMsg.SetActive(true);
        costText.SetActive(false);
        FindObjectOfType<PlayerController>().canMove = true; // Re-enable player movement
    }
}

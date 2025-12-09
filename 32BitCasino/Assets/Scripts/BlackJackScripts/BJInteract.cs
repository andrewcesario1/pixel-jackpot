using TMPro;
using UnityEngine;

public class BJInteract : Interactable
{
    public GameObject bjPanel; // The slot machine UI panel
    public GameObject bjInfoPanel; // The slot machine UI panel
    private bool isPlayerInRange = false; // Track if player is colliding with the slot machine
    public GameObject interactMsg;
    

    void Start()
    {
        interactMsg.SetActive(false);
    }
    // Override the Interact method to define what happens when the player interacts
    public override void Interact()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        
        // Check if the current objective is the first one (index 0) and is incomplete
        if (playerController.currentObjectiveIndex == 1 && !playerController.objectives[1].IsComplete)
        {
            Debug.Log("complete");
            playerController.CompleteCurrentObjective();
        }

        Debug.Log("Player is interacting with the bj table.");
        interactMsg.SetActive(false);
        // Open the slot machine UI
        bjPanel.SetActive(true);
        // Optionally, disable player movement, etc.
        playerController.canMove = false; // Disable player movement
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
        Debug.Log("Player is in range of the bj table.");
        interactMsg.SetActive(true);
        isPlayerInRange = true;
    }

    // When the player exits the collider, set the flag to false
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Player left the bj table.");
        interactMsg.SetActive(false);
        isPlayerInRange = false;
    }

    public void OpenBJInfoPanel()
    {
        bjInfoPanel.SetActive(true); // Hide the slot machine UI panel
    }

    public void CloseBJInfoPanel()
    {
        bjInfoPanel.SetActive(false); // Hide the slot machine UI panel
    }
    
    public void CloseBJPanel()
    {
        bjPanel.SetActive(false); // Hide the slot machine UI panel
        interactMsg.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = true; // Re-enable player movement
    }
}

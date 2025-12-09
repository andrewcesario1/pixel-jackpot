using TMPro;
using UnityEngine;

public class SlotMachineFree : SlotMachine
{
    // Override the Interact method to define what happens when the player interacts
    public override void Interact()
    {
        Debug.Log("Player is interacting with the slot machine.");
        
        PlayerController playerController = FindObjectOfType<PlayerController>();
        
        // Check if the current objective is the first one (index 0) and is incomplete
        if (playerController.currentObjectiveIndex == 0 && !playerController.objectives[0].IsComplete)
        {
            playerController.CompleteCurrentObjective();
        }

        interactMsg.SetActive(false);
        slotMachineUI.SetActive(true);
        costText.SetActive(true);
        playerController.canMove = false; // Disable player movement
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player is in range of the slot machine.");
        interactMsg.SetActive(true);
        interactMsg.GetComponent<TMP_Text>().text = "Press Space to Interact with Free Slot";
        isPlayerInRange = true;
    }

    // When the player exits the collider, set the flag to false
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Player left the slot machine area.");
        interactMsg.SetActive(false);
        interactMsg.GetComponent<TMP_Text>().text = "Press Space to Interact";
        isPlayerInRange = false;
    }
}

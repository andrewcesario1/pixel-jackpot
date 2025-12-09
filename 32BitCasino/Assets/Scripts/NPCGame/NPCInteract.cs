using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : Interactable
{
    public GameObject NPCMsgPanel; // The panel displaying the roulette UI
    public GameObject NPCGamePanel; // The panel displaying the roulette UI

    public GameObject interactMsg;

    public GameObject NPCGameInfoPanel;

    private bool isPlayerInRange = false;

    void Start()
    {
        interactMsg.SetActive(false);
    }

    public override void Interact()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        
        // Check if the current objective is the first one (index 0) and is incomplete
        if (playerController.currentObjectiveIndex == 3 && !playerController.objectives[3].IsComplete)
        {
            Debug.Log("complete");
            playerController.CompleteCurrentObjective();
        }

        Debug.Log("Player is interacting with the NPC.");
        interactMsg.SetActive(false);
        NPCMsgPanel.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = false; // Disable player movement
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }
    // When the player enters the collider, set the flag to true
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player is in range of the NPC.");
        interactMsg.SetActive(true);
        isPlayerInRange = true;
    }

    // When the player exits the collider, set the flag to false
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Player left the NPC.");
        interactMsg.SetActive(false);
        isPlayerInRange = false;
    }

    public void CloseNPCMsgPanel()
    {
        NPCMsgPanel.SetActive(false);
        interactMsg.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = true; // Enable player movement
    }

    public void OpenNPCGamePanel()
    {
        NPCMsgPanel.SetActive(false);
        NPCGamePanel.SetActive(true);
        interactMsg.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = false; // Enable player movement
    }
    public void CloseNPCGamePanel()
    {
        NPCGamePanel.SetActive(false);
        interactMsg.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = true; // Enable player movement
    }


    
    public void OpenNPCGameInfoPanel()
    {
        NPCGameInfoPanel.SetActive(true); // Hide the slot machine UI panel
    }

    public void CloseNPCGameInfoPanel()
    {
        NPCGameInfoPanel.SetActive(false); // Hide the slot machine UI panel
    }
    
}

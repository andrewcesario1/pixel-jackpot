using UnityEngine;

public class RouletteInteract : Interactable
{
    public GameObject roulettePanel; // The panel displaying the roulette UI
    public GameObject interactMsg;

    public GameObject rouletteInfoPanel;

    private bool isPlayerInRange = false;

    void Start()
    {
        interactMsg.SetActive(false);
    }

    public override void Interact()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        
        // Check if the current objective is the first one (index 0) and is incomplete
        if (playerController.currentObjectiveIndex == 1 && !playerController.objectives[1].IsComplete)
        {
            Debug.Log("complete");
            playerController.CompleteCurrentObjective();
        }

        Debug.Log("Player is interacting with the roulette table.");
        interactMsg.SetActive(false);
        roulettePanel.SetActive(true);
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

    public void CloseRoulettePanel()
    {
        roulettePanel.SetActive(false);
        interactMsg.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = true; // Enable player movement
    }
    
    public void OpenRouletteInfoPanel()
    {
        rouletteInfoPanel.SetActive(true); // Hide the slot machine UI panel
    }

    public void CloseRouletteInfoPanel()
    {
        rouletteInfoPanel.SetActive(false); // Hide the slot machine UI panel
    }
    
}

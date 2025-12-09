using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VIPGateInteract : Interactable
{
    public GameObject VIPGatePanel; // The panel displaying the roulette UI
    public GameObject interactMsg;
    public Button vipPurchaseButton;     // The button to purchase VIP
    public TMP_Text feedbackText;        // Text to display feedback messages (e.g., "Insufficient Funds")
    public PlayerController player;      // Reference to the PlayerController script

    private bool isPlayerInRange = false;

    void Start()
    {
        interactMsg.SetActive(false);
    }

    public override void Interact()
    {
        Debug.Log("Player is interacting with the roulette table.");
        interactMsg.SetActive(false);
        VIPGatePanel.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = false; // Disable player movement
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    public void BuyVIPAccess()
    {
        if (player.playerMoney >= 700 && !player.hasVIPAccess)
        {
            // Deduct money and grant VIP access
            player.SubtractMoney(700);
            player.hasVIPAccess = true;
            player.VIPBarrier.SetActive(false); // Remove the barrier
            feedbackText.text = "VIP Access Purchased!"; // Success feedback
            vipPurchaseButton.interactable = false; // Disable the purchase button

            player.SaveData(); // Save player progress

            // Complete the "Buy VIP Access" objective if applicable
            if (player.currentObjectiveIndex < player.objectives.Count &&
                player.objectives[player.currentObjectiveIndex].Title == "Buy VIP Access")
            {
                player.CompleteCurrentObjective();
            }

            CloseVIPGatePanel(); // Close the panel after successful purchase
        }
        else
        {
            // Show insufficient funds message
            feedbackText.text = "Insufficient Funds!";
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

    public void CloseVIPGatePanel()
    {
        VIPGatePanel.SetActive(false);
        interactMsg.SetActive(true);
        FindObjectOfType<PlayerController>().canMove = true; // Enable player movement
    }
    
}

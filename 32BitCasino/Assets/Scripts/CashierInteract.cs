using TMPro;
using UnityEngine;
using UnityEngine.UI; // For Button

public class Cashier : Interactable
{
    public GameObject StoreUI; // The shop UI panel
    public GameObject interactMsg;
    public TMP_Text feedbackText; // Text element for feedback
    public PlayerController player; // Reference to the playerController
    public Button costReductionButton; // For cost reduction item purchase
    public TMP_Text costReductionText;
    public Button speedBoostButton; // For Speed Boost purchase
    public Button boostedWinningsButton; // For Boosted Winnings purchase
    public TMP_Text speedBoostText;
    public TMP_Text boostedWinningsText;

    public int speedBoostPrice = 300; // Price for Speed Boost
    public int boostedWinningsPrice = 400; // Price for Boosted Winnings
    public int costReductionPrice = 1000; // Cost of the item

    private bool isPlayerInRange = false; // Track if player is near the shop

    // Open the shop UI
    public override void Interact()
    {
        interactMsg.SetActive(false);
        StoreUI.SetActive(true);
        player.canMove = false; // Disable player movement

        feedbackText.text = "";

        // Check VIP access status each time the panel is opened
        UpdateButtonsState();
    }
    private void UpdateButtonsState()
    {
        // Cost Reduction Button state
        costReductionButton.interactable = !player.isCostReductionActive;
        costReductionText.text = player.isCostReductionActive ? "Active" : $"${costReductionPrice}";

        // Speed Boost Button state
        speedBoostButton.interactable = !player.isSpeedBoostActive;
        speedBoostText.text = player.isSpeedBoostActive ? "Active" : $"${speedBoostPrice}";

        // Boosted Winnings Button state
        boostedWinningsButton.interactable = !player.isBoostedWinningsActive;
        boostedWinningsText.text = player.isBoostedWinningsActive ? "Active" : $"${boostedWinningsPrice}";
    }


    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    public void BuyCostReduction()
    {
        if (player.playerMoney >= costReductionPrice && !player.isCostReductionActive)
        {
            player.SubtractMoney(costReductionPrice);
            player.ActivateCostReduction();

            UpdateButtonsState();
        }
        else
        {
            feedbackText.text = "Insufficient Funds!";
        }
    }

    public void BuySpeedBoost()
    {
        if (player.playerMoney >= speedBoostPrice && !player.isSpeedBoostActive)
        {
            player.SubtractMoney(speedBoostPrice);
            player.ActivateSpeedBoost();

            UpdateButtonsState();
        }
        else
        {
            feedbackText.text = "Insufficient Funds!";
        }
    }

    public void BuyBoostedWinnings()
    {
        if (player.playerMoney >= boostedWinningsPrice && !player.isBoostedWinningsActive)
        {
            player.SubtractMoney(boostedWinningsPrice);
            player.ActivateBoostedWinnings();

            UpdateButtonsState();
        }
        else
        {
            feedbackText.text = "Insufficient Funds!";
        }
    }

    public void CloseCashierUI()
    {
        StoreUI.SetActive(false); // Hide the shop UI
        interactMsg.SetActive(true);
        player.canMove = true; // Re-enable player movement
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        interactMsg.SetActive(true);
        isPlayerInRange = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interactMsg.SetActive(false);
        isPlayerInRange = false;
    }
}

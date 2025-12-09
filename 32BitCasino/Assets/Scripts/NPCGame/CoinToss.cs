using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinTossGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text betAmountText;      // Bet amount display
    public TMP_Text payoutMessageText; // Payout display

    public GameObject headsTailsButtons; // GameObject holding Heads and Tails buttons
    public Image coinImage; 
    public TMP_Text resultText;        // Coin result display (Heads/Tails)

    [Header("Coin Images")]
    public Sprite headsImage;  // Sprite for Heads
    public Sprite tailsImage;  // Sprite for Tails

    [Header("Settings")]
    public int betIncrement = 500; // Adjustable bet increment in the Editor

    [Header("References")]
    public PlayerController playerController; // Reference to the PlayerController script

    private int currentBet = 0;
    private bool isHeads; // Player's choice (true = heads, false = tails)

    public AudioClip CoinTossSound;

    private void Start()
    {
        ResetGame();
    }

    public void IncreaseBet()
    {
        if (playerController.playerMoney >= betIncrement)
        {
            // Subtract bet amount from player's balance
            playerController.SubtractMoney(betIncrement);
            currentBet += betIncrement;

            // Update UI
            betAmountText.text = $"Bet: ${currentBet}";

            // Enable Heads/Tails buttons after the first bet
            if (!headsTailsButtons.activeSelf)
            {
                headsTailsButtons.SetActive(true);
            }

            payoutMessageText.gameObject.SetActive(false);
            coinImage.gameObject.SetActive(false);
            resultText.text = "";
        }
        else
        {
            // Optional: Show message if the player can't bet more
            Debug.Log("Not enough money to increase bet!");
        }
    }

    public void ChooseHeads()
    {
        isHeads = true;
        StartCoinFlip();
    }

    public void ChooseTails()
    {
        isHeads = false;
        StartCoinFlip();
    }

    private void StartCoinFlip()
    {
        GameAudioManager.Instance.PlaySoundEffect(CoinTossSound);
        // Disable choice buttons and show the coin image
        headsTailsButtons.SetActive(false);
        coinImage.gameObject.SetActive(true);

        // Start the coin flip animation
        StartCoroutine(CoinFlipCoroutine());
    }

    private IEnumerator CoinFlipCoroutine()
    {
        // Simulate coin flipping
        int flipCount = Random.Range(10, 20); // Number of flips
        for (int i = 0; i < flipCount; i++)
        {
            coinImage.sprite = (i % 2 == 0) ? headsImage : tailsImage;
            yield return new WaitForSeconds(0.1f);
        }

        // Determine result
        bool coinResult = Random.Range(0, 2) == 0; // 0 = heads, 1 = tails
        coinImage.sprite = coinResult ? headsImage : tailsImage;

        // Display result
        resultText.text = coinResult ? "Heads" : "Tails";

        // Determine win/loss
        if (isHeads == coinResult)
        {
            payoutMessageText.text = $"+${currentBet * 2}";
            playerController.AddMoney(currentBet * 2); // Double the bet amount as payout
        }
        
        payoutMessageText.gameObject.SetActive(true);

        // Reset the bet for the next round
        currentBet = 0;
    }

    private void ResetGame()
    {
        currentBet = 0;
        betAmountText.text = "Bet: $0";

        headsTailsButtons.SetActive(false);
        coinImage.gameObject.SetActive(false);
        payoutMessageText.gameObject.SetActive(false);
        resultText.text = "";
    }
}

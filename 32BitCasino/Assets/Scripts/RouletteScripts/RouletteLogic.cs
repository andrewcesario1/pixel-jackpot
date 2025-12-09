using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteLogic : MonoBehaviour
{
    public TMP_Text landedNumberText; // Displays the number landed on
    public TMP_Text payoutText; // Displays payout amount
    public TMP_Text totalBetAmountText; // Displays the total bet amount

    public Button spinButton; // Button to spin the wheel
    public Button greenButton, redButton, blackButton, firstButton, secondButton, thirdButton;
    public TMP_Text greenBetText, redBetText, blackBetText, firstBetText, secondBetText, thirdBetText; // Bet texts for each button

    public int chipValue = 50; // Value of each chip
    public int maxBetAmount = 500; // Maximum allowed bet per round

    private int totalBetAmount = 0; // Total amount the player has bet
    private Dictionary<string, int> buttonBets = new Dictionary<string, int>(); // Tracks bets for each button
    private int winningNumber;

    public AudioClip rouletteSound;

    private PlayerController playerController; // Reference to PlayerController for balance

    void Start()
    {
        ResetGame();

        // Set up button listeners
        greenButton.onClick.AddListener(() => PlaceBet("Green", greenBetText));
        redButton.onClick.AddListener(() => PlaceBet("Red", redBetText));
        blackButton.onClick.AddListener(() => PlaceBet("Black", blackBetText));
        firstButton.onClick.AddListener(() => PlaceBet("1st", firstBetText));
        secondButton.onClick.AddListener(() => PlaceBet("2nd", secondBetText));
        thirdButton.onClick.AddListener(() => PlaceBet("3rd", thirdBetText));

        spinButton.onClick.AddListener(SpinWheel);
        spinButton.interactable = false;
    }

    public void PlaceBet(string betType, TMP_Text betText)
    {

        landedNumberText.gameObject.SetActive(false);
        payoutText.gameObject.SetActive(false);

        if (totalBetAmount + chipValue > maxBetAmount)
        {
            Debug.Log("Cannot bet more than the maximum allowed amount.");
            return;
        }

        // Add bet to the selected button
        if (!buttonBets.ContainsKey(betType))
        {
            buttonBets[betType] = 0;
        }

        buttonBets[betType] += chipValue;
        totalBetAmount += chipValue;

        // Update UI
        betText.text = $"Bet: ${buttonBets[betType]}";
        betText.gameObject.SetActive(true);
        totalBetAmountText.text = $"Total Bet: ${totalBetAmount}";

        // Enable spin button
        spinButton.interactable = true;
    }

    public void SpinWheel()
    {
        if (totalBetAmount == 0)
        {
            Debug.Log("No bets placed!");
            return;
        }

        FindObjectOfType<PlayerController>().SubtractMoney(totalBetAmount);

        GameAudioManager.Instance.PlaySoundEffect(rouletteSound);

        // Start the spinning animation
        StartCoroutine(SpinAnimation());
    }

    private IEnumerator SpinAnimation()
    {
        float spinDuration = 2.26f; // Total time for the spinning animation
        float elapsedTime = 0f;
        int tempNumber;
        string tempColor;

        yield return new WaitForSeconds(0.1f);
        while (elapsedTime < spinDuration)
        {
            // Generate a random number and determine its color
            tempNumber = Random.Range(0, 37);
            if (tempNumber == 0)
            {
                tempColor = "<color=green>";
            }
            else if (tempNumber % 2 == 0)
            {
                tempColor = "<color=red>";
            }
            else
            {
                tempColor = "<color=black>";
            }

            // Update the landedNumberText with the current random number and color
            landedNumberText.text = $"{tempColor}{tempNumber}</color>";
            landedNumberText.gameObject.SetActive(true);

            // Wait briefly before the next number
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.1f;
        }

        // Determine the final winning number
        winningNumber = Random.Range(0, 37);

        // Set the final result with the correct color
        if (winningNumber == 0)
        {
            landedNumberText.text = $"<color=green>{winningNumber}</color>";
        }
        else if (winningNumber % 2 == 0)
        {
            landedNumberText.text = $"<color=red>{winningNumber}</color>";
        }
        else
        {
            landedNumberText.text = $"<color=black>{winningNumber}</color>";
        }

        landedNumberText.gameObject.SetActive(true);

        // Proceed with payout calculation
        CalculatePayout();
        ResetGame();
    }

    private void CalculatePayout()
    {
        int payout = 0;

        // Calculate payout based on bets
        if (winningNumber == 0 && buttonBets.ContainsKey("Green"))
        {
            payout += buttonBets["Green"] * 14;
        }
        if (winningNumber % 2 == 0 && buttonBets.ContainsKey("Red"))
        {
            payout += buttonBets["Red"] * 2;
        }
        if (winningNumber % 2 == 1 && buttonBets.ContainsKey("Black"))
        {
            payout += buttonBets["Black"] * 2;
        }
        if (winningNumber >= 1 && winningNumber <= 12 && buttonBets.ContainsKey("1st"))
        {
            payout += buttonBets["1st"] * 3;
        }
        if (winningNumber >= 13 && winningNumber <= 24 && buttonBets.ContainsKey("2nd"))
        {
            payout += buttonBets["2nd"] * 3;
        }
        if (winningNumber >= 25 && winningNumber <= 36 && buttonBets.ContainsKey("3rd"))
        {
            payout += buttonBets["3rd"] * 3;
        }

        // Update payout text and add money to the player
        if (payout > 0)
        {
            payoutText.text = $"+${payout}";
            payoutText.color = Color.green;
            FindObjectOfType<PlayerController>().AddMoney(payout);
        }
        else
        {
            payoutText.text = "";
        }

        payoutText.gameObject.SetActive(true);

        // Start coroutine to hide the payout text after 5 seconds
        StartCoroutine(HidePayoutText());
    }

    // Coroutine to hide payout text
    private IEnumerator HidePayoutText()
    {
        yield return new WaitForSeconds(3); // Wait for 5 seconds
        payoutText.gameObject.SetActive(false); // Hide the text
    }

    private void ResetGame()
    {
        // Reset bets and UI
        totalBetAmount = 0;
        buttonBets.Clear();
        totalBetAmountText.text = "Total Bet: $0";

        greenBetText.gameObject.SetActive(false);
        redBetText.gameObject.SetActive(false);
        blackBetText.gameObject.SetActive(false);
        firstBetText.gameObject.SetActive(false);
        secondBetText.gameObject.SetActive(false);
        thirdBetText.gameObject.SetActive(false);

        spinButton.interactable = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Buttons
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    public Button amountBtn;
    public Button DblBtn;

    public GameScript playerScript;
    public GameScript dealerScript;
    public TMP_Text balanceText;
    public TMP_Text betAmountText;
    public TMP_Text handText;
    public TMP_Text dealerHandText;
    public TMP_Text screenText;
    public TMP_Text winAmountTxt;

    public GameObject hideCard;

    public AudioClip dealSound;

    public PlayerController playerController; // Reference to PlayerController for balance
    int numBetAmount = 0;

    void Start()
    {
        // Initialize PlayerController reference
        playerController = FindObjectOfType<PlayerController>();

        DblBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        betBtn.onClick.AddListener(() => betClicked());
        standBtn.onClick.AddListener(() => standClicked());
        hitBtn.onClick.AddListener(() => hitClicked());
        amountBtn.onClick.AddListener(() => amountBtnClicked());
        DblBtn.onClick.AddListener(() => dblClicked());

        // Set initial balance display
        UpdateBalanceText();

        // Set dealer and player card Images to invisible initially
        for (int i = 0; i < playerScript.hand.Length - 2; i++)
        {
            playerScript.hand[i + 2].GetComponent<Image>().enabled = false;
            dealerScript.hand[i + 2].GetComponent<Image>().enabled = false;
        }
    }

    private void dblClicked()
    {
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        DblBtn.gameObject.SetActive(false);

        playerScript.UpdateMoney(-numBetAmount);
        numBetAmount = numBetAmount * 2;
        betAmountText.text = $"Bet: ${numBetAmount.ToString()}";
        balanceText.text = playerScript.GetBalance().ToString();

        if(playerScript.cardIndex <=10)
        {
            playerScript.GetCard();
            handText.text = $"Hand: {playerScript.handValue.ToString()}";
            if(playerScript.handValue > 20)
            {
                GameFinished();
            }
        }

        standClicked();

    }

    private void amountBtnClicked()
    {
        int currentBalance = playerController.playerMoney;

        if (currentBalance >= 100)
        {
            numBetAmount += 100;
            playerController.SubtractMoney(100); // Use SubtractMoney to update balance
            betAmountText.text = $"Bet: ${numBetAmount.ToString()}";
            UpdateBalanceText();
        }
        else if (currentBalance == 0 && numBetAmount == 0)
        {
            screenText.text = "Out of Money - Get a job!";
        }
    }

    private void hitClicked()
    {
        DblBtn.gameObject.SetActive(false);
        if(playerScript.cardIndex <=10)
        {
            playerScript.GetCard();
            handText.text = $"Hand: {playerScript.handValue.ToString()}";
            if(playerScript.handValue > 20)
            {
                GameFinished();
            }
        }
    }

    private void standClicked()
    {
        dealerHandText.text = $"Hand: {dealerScript.handValue}"; // Reveal dealer's hand total
        Debug.Log("stand clicked");
        hideCard.gameObject.SetActive(false);
        HitDealer();
    }

    private void HitDealer()
    {
        dealerHandText.gameObject.SetActive(true);
        while(dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealerHandText.text = $"Hand: {dealerScript.handValue.ToString()}";
            if(dealerScript.handValue > 20)
            {
                GameFinished();
                return;
            }
        }
        GameFinished();
    }

    private void betClicked()
    {
        GameAudioManager.Instance.PlaySoundEffect(dealSound);
        playerScript.ResetGame();
        dealerScript.ResetGame();

        // Reset deck and start new round
        GameObject.Find("deck").GetComponent<deck>().Shuffle();

        hideCard.gameObject.SetActive(true);
        amountBtn.interactable = false;
        screenText.gameObject.SetActive(false);
        winAmountTxt.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        if (playerScript.GetBalance() >= numBetAmount)
        {
            DblBtn.gameObject.SetActive(true);
        }

        playerScript.StartHand();
        dealerScript.StartHand();

        hideCard.GetComponent<Image>().enabled = true; // Adjusted for Image

        // Update hand total
        handText.text = $"Hand: {playerScript.handValue.ToString()}";
        dealerHandText.text = "Hand: ?";
        betAmountText.text = $"Bet: ${numBetAmount.ToString()}";
        balanceText.text = playerScript.GetBalance().ToString();

        // Check for Blackjack
        if (playerScript.handValue == 21)
        {
            GameFinished(); // End game immediately if Blackjack
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateBalanceText()
    {
        balanceText.text = playerController.playerMoney.ToString(); // Fetch balance directly from PlayerController
    }

    void GameFinished()
    {
        Debug.Log("game finished");
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;

        winAmountTxt.text = "";

        if (playerBust && dealerBust)
        {
            screenText.text = "All Bust";
            playerController.AddMoney(numBetAmount); // Return bet if both bust
        }
        else if (playerBust)
        {
            screenText.text = "Dealer wins!";
        }
        else if (dealerBust)
        {
            screenText.text = "You win!";
            int winnings = numBetAmount * 2;
            winAmountTxt.text = $"+${winnings}";
            Debug.Log("Bust");
            playerController.AddMoney(winnings); // Player wins
        }
        else if (dealerScript.handValue > playerScript.handValue)
        {
            screenText.text = "Dealer wins!";
        }
        else if (playerScript.handValue > dealerScript.handValue)
        {
            screenText.text = "You win!";
            int winnings = numBetAmount * 2;
            winAmountTxt.text = $"+${winnings}";
            playerController.AddMoney(winnings); // Player wins
        }
        else if (playerScript.handValue == dealerScript.handValue)
        {
            screenText.text = "Push";
            winAmountTxt.text = $"+${numBetAmount}";
            playerController.AddMoney(numBetAmount); // Return bet on tie
        }

        // UI updates after game ends
        hitBtn.gameObject.SetActive(false);
        standBtn.gameObject.SetActive(false);
        DblBtn.gameObject.SetActive(false);
        betBtn.gameObject.SetActive(true);
        screenText.gameObject.SetActive(true);
        winAmountTxt.gameObject.SetActive(true);
        dealerHandText.gameObject.SetActive(true);
        hideCard.GetComponent<Image>().enabled = false;

        // Update balance display after game concludes
        UpdateBalanceText();

        // Enable betting button and reset bet amount after payout
        amountBtn.interactable = true;
        betAmountText.text = $"Bet: ${numBetAmount}";

        // Only reset numBetAmount at the very end
        numBetAmount = 0;
        betAmountText.text = $"Bet: $0";
    }


}

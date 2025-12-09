using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import UI namespace for Image

public class GameScript : MonoBehaviour
{
    public CardScript cardScript;
    public deck deckScript;

    public int handValue = 0;

    private int balance = 1000;

    public GameObject[] hand;
    public int cardIndex = 0;
    public int aceCount = 0;

    List<CardScript> aceList = new List<CardScript>();

    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    public int GetCard()
    {
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());

        if (cardValue == -1) 
        {
            Debug.LogError("Deck is out of cards.");
            return handValue; // Early return if no card was dealt.
        }

        hand[cardIndex].GetComponent<Image>().enabled = true; // Enable the Image component
        handValue += cardValue;

        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }

        CheckAce();
        cardIndex++;
        return handValue;
    }

    public void UpdateMoney(int amount)
    {
        balance += amount;
    }

    public int GetBalance()
    {
        return balance;
    }

    public void CheckAce()
    {
        foreach(CardScript ace in aceList)
        {
            if(handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
                ace.SetValue(11);
                handValue += 10;
            }
            else if(handValue > 21 && ace.GetValueOfCard() == 11)
            {
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    public void ResetGame()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Image>().enabled = false; // Disable the Image component instead of Renderer
        }

        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}

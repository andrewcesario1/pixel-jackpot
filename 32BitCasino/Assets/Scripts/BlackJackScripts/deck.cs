using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deck : MonoBehaviour
{
    public Sprite[] cards;
    private int[] cardvalues = new int[53];
    private int currentIndex = 0;
    
    void Start()
    {
        GetCardValues();
    }

    void GetCardValues()
    {
        int num = 0;
        for(int i = 0; i < cards.Length; i++)
        {
            num = i % 13;
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardvalues[i] = num++;
        }
        currentIndex = 1;
    }

    public void Shuffle()
    {
        for(int i = cards.Length - 1; i > 0; i--)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * (cards.Length - 1)) + 1;
            Sprite face = cards[i];
            cards[i] = cards[j];
            cards[j] = face;

            int value = cardvalues[i];
            cardvalues[i] = cardvalues[j];
            cardvalues[j] = value;
        }
        currentIndex = 1;
    }

    public int DealCard(CardScript cardScript)
    {
        if (currentIndex >= cards.Length)
        {
            Debug.Log("Deck is out of cards. Reshuffling...");
            Shuffle();
        }

        cardScript.SetImage(cards[currentIndex]);
        cardScript.SetValue(cardvalues[currentIndex++]);
        return cardScript.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cards[0];
    }
}

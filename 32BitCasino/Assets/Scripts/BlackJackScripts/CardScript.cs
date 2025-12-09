using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import UI namespace for Image

public class CardScript : MonoBehaviour
{
    public int value = 0;
    private Image cardImage; // Reference to the Image component

    void Awake()
    {
        cardImage = GetComponent<Image>(); // Assign the Image component
    }

    public int GetValueOfCard()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public string GetImageName()
    {
        return cardImage.sprite.name;
    }

    public void SetImage(Sprite newSprite)
    {
        cardImage.sprite = newSprite;
    }

    public void ResetCard()
    {
        Sprite back = GameObject.Find("deck").GetComponent<deck>().GetCardBack();
        cardImage.sprite = back;
        value = 0;
    }
}

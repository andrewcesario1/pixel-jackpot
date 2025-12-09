using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public PlayerController player; // Reference to the playerController
    public TMP_Text moneyText; // Reference to the UI Text element

    void Update()
    {
        // Update the money display with the current player's money
        moneyText.text = "$" + player.playerMoney.ToString();
    }
}

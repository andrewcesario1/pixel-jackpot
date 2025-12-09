using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReelManager : MonoBehaviour
{
    public Image[] reel1Symbols;
    public Image[] reel2Symbols;
    public Image[] reel3Symbols;

    public Sprite[] symbolSprites;

    public float changeSpeed = 0.1f;
    public float spinDuration = 3.0f;

    public int spinCost = 50;

    public int[] payoutMultipliers; // Payout for 3-in-a-row
    public int[] twoSymbolPayoutMultipliers; // Payout for 2-in-a-row

    private bool isSpinning = false;

    private int[] previousSymbolIndicesReel1;
    private int[] previousSymbolIndicesReel2;
    private int[] previousSymbolIndicesReel3;

    public PlayerController player;
    public TMP_Text winText;
    public TMP_Text winAmountText;

    public AudioClip reelSpinSound;

    private bool freeSpin = false;

    void Start()
    {
        InitializePreviousSymbolIndices();

        // Hide win texts initially
        winText.gameObject.SetActive(false);
        winAmountText.gameObject.SetActive(false);
    }

    private void InitializePreviousSymbolIndices()
    {
        previousSymbolIndicesReel1 = new int[reel1Symbols.Length];
        previousSymbolIndicesReel2 = new int[reel2Symbols.Length];
        previousSymbolIndicesReel3 = new int[reel3Symbols.Length];

        for (int i = 0; i < reel1Symbols.Length; i++)
        {
            previousSymbolIndicesReel1[i] = -1;
            previousSymbolIndicesReel2[i] = -1;
            previousSymbolIndicesReel3[i] = -1;
        }
    }

    public void StartSpin()
    {
        // Check if GameAudioManager.Instance exists and if the clip is assigned
        if (GameAudioManager.Instance != null)
        {
            GameAudioManager.Instance.PlaySoundEffect(reelSpinSound);
        }
        else
        {
            Debug.LogWarning("GameAudioManager instance or spinSound AudioClip is missing.");
        }

        if (!isSpinning && player.playerMoney >= spinCost)
        {
            winText.gameObject.SetActive(false);
            winAmountText.gameObject.SetActive(false);

            player.SubtractMoney(spinCost);
            isSpinning = true;
            StartCoroutine(SpinReels());
        }
        else
        {
            Debug.Log("Not enough money to spin.");
        }
    }

    private IEnumerator SpinReels()
    {
        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            ChangeSymbols(reel1Symbols, previousSymbolIndicesReel1);
            ChangeSymbols(reel2Symbols, previousSymbolIndicesReel2);
            ChangeSymbols(reel3Symbols, previousSymbolIndicesReel3);

            elapsedTime += changeSpeed;
            yield return new WaitForSeconds(changeSpeed);
        }

        StopReels();
    }

    private void ChangeSymbols(Image[] reelSymbols, int[] previousSymbolIndices)
    {
        for (int i = 0; i < reelSymbols.Length; i++)
        {
            int newSymbolIndex;

            do
            {
                newSymbolIndex = Random.Range(0, symbolSprites.Length);
            }
            while (newSymbolIndex == previousSymbolIndices[i]);

            reelSymbols[i].sprite = symbolSprites[newSymbolIndex];
            previousSymbolIndices[i] = newSymbolIndex;
        }
    }

    private void StopReels()
    {
        int finalSymbol1 = Random.Range(0, symbolSprites.Length);
        int finalSymbol2 = Random.Range(0, symbolSprites.Length);
        int finalSymbol3 = Random.Range(0, symbolSprites.Length);

        reel1Symbols[1].sprite = symbolSprites[finalSymbol1];
        reel2Symbols[1].sprite = symbolSprites[finalSymbol2];
        reel3Symbols[1].sprite = symbolSprites[finalSymbol3];

        Debug.Log($"Reel 1: {symbolSprites[finalSymbol1].name}");
        Debug.Log($"Reel 2: {symbolSprites[finalSymbol2].name}");
        Debug.Log($"Reel 3: {symbolSprites[finalSymbol3].name}");

        // Check for matches
        if (finalSymbol1 == finalSymbol2 && finalSymbol2 == finalSymbol3)
        {
            ApplyReward(finalSymbol1, payoutMultipliers);
        }
        else if (finalSymbol1 == finalSymbol2 || finalSymbol1 == finalSymbol3 || finalSymbol2 == finalSymbol3)
        {
            // Two matching symbols
            int matchingSymbol = (finalSymbol1 == finalSymbol2) ? finalSymbol1 : finalSymbol3;
            ApplyReward(matchingSymbol, twoSymbolPayoutMultipliers);
        }
        else
        {
            Debug.Log("You lost. Try again.");
        }

        isSpinning = false;
    }

    private void ApplyReward(int symbolIndex, int[] multipliers)
    {
        if(spinCost == 0)
        {
            freeSpin = true;
        }

        if (symbolIndex < multipliers.Length)
        {
            int rewardMultiplier = multipliers[symbolIndex];

            // Only display reward if the multiplier is greater than 0
            if (rewardMultiplier > 0)
            {
                int reward;
                if(freeSpin)
                {
                    reward = 5 * rewardMultiplier;
                }
                else
                {
                    reward = spinCost * rewardMultiplier;
                }
                player.AddMoney(reward);

                // Display win messages
                winText.text = "You Won!";
                winAmountText.text = $"+${reward}";
                winText.gameObject.SetActive(true);
                winAmountText.gameObject.SetActive(true);

                Debug.Log($"You won! Reward: ${reward}");
            }
            else
            {
                Debug.Log("No payout for these symbols.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid symbol index for payout.");
        }

    }

}

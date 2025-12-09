using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    public bool movingRight;
    public bool movingLeft;
    public bool movingUp;
    public bool movingDown;

    public int playerDirection;

    public GameObject faceR;
    public GameObject faceL;
    public GameObject faceU;
    public GameObject faceD;

    public Animator playerAnimations;

    // This variable will control if the player can move
    public bool canMove = true;

    // Money system
    public int playerMoney = 500;
    public bool hasVIPAccess = false;
    public GameObject VIPBarrier;
    public GameObject gameOverPanel;
    public GameObject HUD;
    public TMP_Text objectiveText;
    public GameObject objCompleteObj;
    public TMP_Text objectiveCompleteText;
    
    // New tutorial-related variables
    public GameObject tutorialPanel; // Reference to the Tutorial UI panel
    public bool hasSeenTutorial;

    // Objective-related variables
    public GameObject objObject;
    public List<Objective> objectives;
    public int currentObjectiveIndex;
    public AudioClip objectiveCompleteSound;

    // Cost reduction item variables
    public bool isCostReductionActive = false;
    public GameObject costReductionUI; // The item image on the top-right
    public TMP_Text costReductionTimerText;
    public float costReductionDuration = 120f; // 2 minutes, adjustable in the editor
    private float costReductionTimeLeft;

    // Speed Boost item variables
    public bool isSpeedBoostActive = false;
    public GameObject speedBoostUI; // The item image on the top-right
    public TMP_Text speedBoostTimerText;
    public float speedBoostDuration = 120f; // Adjustable in the editor
    private float speedBoostTimeLeft;

    // Boosted Winnings item variables
    public bool isBoostedWinningsActive = false;
    public GameObject boostedWinningsUI; // The item image on the top-right
    public TMP_Text boostedWinningsTimerText;
    public float boostedWinningsDuration = 120f; // Adjustable in the editor
    private float boostedWinningsTimeLeft;

    public Transform activeItemsContainer; // The container with the Vertical Layout Group
    public GameObject speedBoostPrefab; // Prefab for the Speed Boost item
    public GameObject boostedWinningsPrefab; // Prefab for the Boosted Winnings item
    public GameObject costReductionPrefab; // Prefab for the Cost Reduction item

    private Dictionary<string, GameObject> activeItemUIs = new Dictionary<string, GameObject>();


    void Start()
    {
        // Load saved data
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 500);
        hasVIPAccess = PlayerPrefs.GetInt("VIPAccess", 0) == 1;
        hasSeenTutorial = PlayerPrefs.GetInt("HasSeenTutorial", 0) == 1;

        // Set saved audio volumes using AudioManager
        // AudioManager.Instance.SetGameMusicVolume(PlayerPrefs.GetFloat("musicVolume", 1f));
        // AudioManager.Instance.SetSoundEffectVolume(PlayerPrefs.GetFloat("soundEffectVolume", 1f));

        // Load player position if saved
        float posX = PlayerPrefs.GetFloat("PlayerPosX", transform.position.x);
        float posY = PlayerPrefs.GetFloat("PlayerPosY", transform.position.y);
        transform.position = new Vector2(posX, posY);

        if (hasVIPAccess && VIPBarrier != null)
        {
            VIPBarrier.SetActive(false);
        }

        // Show tutorial if it hasn't been seen yet
        if (!hasSeenTutorial)
        {
            ShowTutorial();
        }

        // Check if Cost Reduction is still active
        if (PlayerPrefs.HasKey("CostReductionTimeLeft"))
        {
            isCostReductionActive = true;
            GameObject ui = Instantiate(costReductionPrefab, activeItemsContainer);
            activeItemUIs["CostReduction"] = ui;
            StartCoroutine(CostReductionTimer(ui));
        }

        // Check if Speed Boost is still active
        if (PlayerPrefs.HasKey("SpeedBoostTimeLeft"))
        {
            isSpeedBoostActive = true;
            GameObject ui = Instantiate(speedBoostPrefab, activeItemsContainer);
            activeItemUIs["SpeedBoost"] = ui;
            StartCoroutine(SpeedBoostTimer(ui));
        }

        // Check if Boosted Winnings is still active
        if (PlayerPrefs.HasKey("BoostedWinningsTimeLeft"))
        {
            isBoostedWinningsActive = true;
            GameObject ui = Instantiate(boostedWinningsPrefab, activeItemsContainer);
            activeItemUIs["BoostedWinnings"] = ui;
            StartCoroutine(BoostedWinningsTimer(ui));
        }

        HUD.SetActive(true);
        // Initialize objectives and load saved objective progress
        InitializeObjectives();
        currentObjectiveIndex = PlayerPrefs.GetInt("CurrentObjectiveIndex", 0); // Load saved index or default to 0
        objObject.gameObject.SetActive(true);
        UpdateObjectiveText();
        if(!(currentObjectiveIndex < objectives.Count))
        {
            objObject.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Only allow movement if canMove is true
        if (canMove)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();

            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
            {
                moveInput.y = 0;
                rb2d.velocity = moveInput * moveSpeed * (isSpeedBoostActive ? 1.5f : 1f);

            }
            else
            {
                moveInput.x = 0;
                rb2d.velocity = moveInput * moveSpeed * (isSpeedBoostActive ? 1.5f : 1f);

            }

            // moving right
            if (moveInput.x > 0)
            {
                movingRight = true;
                faceR.SetActive(true);
                faceL.SetActive(false);
                faceD.SetActive(false);
                faceU.SetActive(false);
                playerAnimations.Play("walkR");
            }
            else
            {
                movingRight = false;
            }

            // moving left
            if (moveInput.x < 0)
            {
                movingLeft = true;
                faceL.SetActive(true);
                faceR.SetActive(false);
                faceD.SetActive(false);
                faceU.SetActive(false);
                playerAnimations.Play("walkL");
            }
            else
            {
                movingLeft = false;
            }

            // moving up
            if (moveInput.y > 0)
            {
                movingUp = true;
                faceU.SetActive(true);
                faceD.SetActive(false);
                faceL.SetActive(false);
                faceR.SetActive(false);
                playerAnimations.Play("walkU");
            }
            else
            {
                movingUp = false;
            }

            // moving down
            if (moveInput.y < 0)
            {
                movingDown = true;
                faceD.SetActive(true);
                faceU.SetActive(false);
                faceL.SetActive(false);
                faceR.SetActive(false);
                playerAnimations.Play("walkD");
            }
            else
            {
                movingDown = false;
            }

            // Idle when not moving
            if (moveInput.y == 0 && moveInput.x == 0)
            {
                playerAnimations.Play("playerIdle");
            }
        }
        else
        {
            // Stop player movement if canMove is false
            rb2d.velocity = Vector2.zero;
        }

        // if (playerMoney <= 0)
        // {
        //     TriggerGameOver();
        // }
    }

    private void InitializeObjectives()
    {
        objectives = new List<Objective>
        {
            new Objective("Spin For Free!", "Visit the free slot machine by the door", 200),
            new Objective("Visit The Tables", "Locate the table room up the stairs.", 200),
            new Objective("Buy VIP Access", "Purchase VIP access in the top Hallway.", 500),
            new Objective("Speak to Jon", "Speak to Jon in top right of Main Lobby.", 200),
            // Add more objectives as needed
        };
    }

    public void UpdateObjectiveText()
    {
        if (currentObjectiveIndex < objectives.Count)
        {
            Objective currentObjective = objectives[currentObjectiveIndex];

            // Format the text using rich text tags for color and size
            objectiveText.text = currentObjective.IsComplete
                ? $"<color=white><b>{currentObjective.Title}</b></color>\n<size=60%><color=grey>{currentObjective.Description}\nReward: ${currentObjective.RewardAmount} (Completed)</color></size>"
                : $"<color=white><b>{currentObjective.Title}</b></color>\n<size=60%><color=grey>{currentObjective.Description}\n</color><size=60%><color=green>Reward: ${currentObjective.RewardAmount}</color></size>";

            objectiveText.color = currentObjective.IsComplete ? Color.green : Color.grey;
        }
        else
        {

            objectiveText.text = "All Objectives Completed!";
            objectiveText.color = Color.green;
            // challengeMachine.UnlockMachine();
        }
    }


    public void CompleteCurrentObjective()
    {
        if (currentObjectiveIndex < objectives.Count)
        {
            Objective currentObjective = objectives[currentObjectiveIndex];
            currentObjective.Complete();
            AddMoney(currentObjective.RewardAmount);

            // Show objective completion notification
            ShowObjectiveComplete(currentObjective);

            currentObjectiveIndex++;

            PlayerPrefs.SetInt("CurrentObjectiveIndex", currentObjectiveIndex);
            PlayerPrefs.Save();

            UpdateObjectiveText();
            GameAudioManager.Instance.PlaySoundEffect(objectiveCompleteSound);
        }
    }

    private void ShowObjectiveComplete(Objective objective)
    {
        // Format the text with title and reward amount
        objectiveCompleteText.text = $"<color=white><b>{objective.Title}</b></color>\n<size=60%><color=green>Reward: ${objective.RewardAmount}</color></size>";
        
        // Show the notification
        objCompleteObj.SetActive(true);

        // Start the coroutine to hide it after 1.5 seconds
        StartCoroutine(HideObjectiveComplete());
        
    }

    private IEnumerator HideObjectiveComplete()
    {
        yield return new WaitForSeconds(2f);
        objCompleteObj.SetActive(false);
        if(!(currentObjectiveIndex < objectives.Count))
        {
            objObject.gameObject.SetActive(false);
        }
    }

    // private void TriggerGameOver()
    // {
    //     gameOverPanel.SetActive(true); // Show the Game Over screen
    //     canMove = false; // Disable player movement
    // }

    // Call this method to add money
    public void AddMoney(int amount)
    {
        // if (isBoostedWinningsActive && UnityEngine.Random.value <= 0.33f) // 1/3 chance
        // {
        //     amount = Mathf.CeilToInt(amount * 1.5f); // 50% bonus
        // }
        if (isBoostedWinningsActive) // 1/3 chance
        {
            amount = Mathf.CeilToInt(amount * 1.15f); // 50% bonus
        }
        playerMoney += amount;
        SaveData();
    }

    // Call this method to subtract money
    public bool SubtractMoney(int amount)
    {
        if (!HasEnoughMoney(amount))
        {
            Debug.LogWarning("Not enough money to subtract!");
            return false; // Return false if the player doesn't have enough money
        }

        if (isCostReductionActive)
        {
            amount = Mathf.CeilToInt(amount / 2f); // Apply 50% discount
        }

        playerMoney -= amount;
        SaveData();
        return true; // Return true if the money was successfully subtracted
    }


    public bool HasEnoughMoney(int amount)
    {
        return playerMoney >= amount;
    }


    public void ActivateCostReduction()
    {
        if (isCostReductionActive) return;

        isCostReductionActive = true;

        // Instantiate the UI prefab dynamically
        GameObject ui = Instantiate(costReductionPrefab, activeItemsContainer);
        ui.GetComponentInChildren<TMP_Text>().text = $"{Mathf.Ceil(costReductionDuration)}s"; // Set initial timer text
        activeItemUIs["CostReduction"] = ui; // Store reference to the UI element

        StartCoroutine(CostReductionTimer(ui));
    }

    private IEnumerator CostReductionTimer(GameObject ui)
    {
        // Load remaining time if available, otherwise start with the default duration
        float costReductionTimeLeft = PlayerPrefs.GetFloat("CostReductionTimeLeft", costReductionDuration);

        while (costReductionTimeLeft > 0)
        {
            costReductionTimeLeft -= Time.deltaTime;
            ui.GetComponentInChildren<TMP_Text>().text = $"{Mathf.Ceil(costReductionTimeLeft)}s";

            // Save remaining time periodically
            if (Mathf.FloorToInt(costReductionTimeLeft) % 1 == 0)
            {
                PlayerPrefs.SetFloat("CostReductionTimeLeft", costReductionTimeLeft);
                PlayerPrefs.Save();
            }

            yield return null;
        }

        // Deactivate the cost reduction effect
        isCostReductionActive = false;

        // Remove the UI element
        if (activeItemUIs.ContainsKey("CostReduction"))
        {
            Destroy(ui);
            activeItemUIs.Remove("CostReduction");
        }

        // Clear the saved timer when finished
        PlayerPrefs.DeleteKey("CostReductionTimeLeft");
        PlayerPrefs.Save();
    }

    public void ActivateSpeedBoost()
    {
        isSpeedBoostActive = true;

        // Instantiate the UI prefab dynamically
        GameObject ui = Instantiate(speedBoostPrefab, activeItemsContainer);
        ui.GetComponentInChildren<TMP_Text>().text = $"{Mathf.Ceil(speedBoostDuration)}s"; // Set timer text
        activeItemUIs["SpeedBoost"] = ui; // Store reference

        StartCoroutine(SpeedBoostTimer(ui));
    }
    
    private IEnumerator SpeedBoostTimer(GameObject ui)
    {
        // Load remaining time if available, otherwise start with the default duration
        float timeLeft = PlayerPrefs.GetFloat("SpeedBoostTimeLeft", speedBoostDuration);

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            ui.GetComponentInChildren<TMP_Text>().text = $"{Mathf.Ceil(timeLeft)}s";

            // Save remaining time periodically
            if (Mathf.FloorToInt(timeLeft) % 1 == 0)
            {
                PlayerPrefs.SetFloat("SpeedBoostTimeLeft", timeLeft);
                PlayerPrefs.Save();
            }

            yield return null;
        }

        // Deactivate the speed boost effect
        isSpeedBoostActive = false;

        // Remove the UI element
        if (activeItemUIs.ContainsKey("SpeedBoost"))
        {
            Destroy(ui);
            activeItemUIs.Remove("SpeedBoost");
        }

        // Clear the saved timer when finished
        PlayerPrefs.DeleteKey("SpeedBoostTimeLeft");
        PlayerPrefs.Save();
    }

    public void ActivateBoostedWinnings()
    {
        isBoostedWinningsActive = true;

        GameObject ui = Instantiate(boostedWinningsPrefab, activeItemsContainer);
        ui.GetComponentInChildren<TMP_Text>().text = $"{Mathf.Ceil(boostedWinningsDuration)}s";
        activeItemUIs["BoostedWinnings"] = ui;

        StartCoroutine(BoostedWinningsTimer(ui));
    }

    private IEnumerator BoostedWinningsTimer(GameObject ui)
    {
        // Load remaining time if available, otherwise start with the default duration
        float timeLeft = PlayerPrefs.GetFloat("BoostedWinningsTimeLeft", boostedWinningsDuration);

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            ui.GetComponentInChildren<TMP_Text>().text = $"{Mathf.Ceil(timeLeft)}s";

            // Save remaining time periodically
            if (Mathf.FloorToInt(timeLeft) % 1 == 0)
            {
                PlayerPrefs.SetFloat("BoostedWinningsTimeLeft", timeLeft);
                PlayerPrefs.Save();
            }

            yield return null;
        }

        // Deactivate the boosted winnings effect
        isBoostedWinningsActive = false;

        // Remove the UI element
        if (activeItemUIs.ContainsKey("BoostedWinnings"))
        {
            Destroy(ui);
            activeItemUIs.Remove("BoostedWinnings");
        }

        // Clear the saved timer when finished
        PlayerPrefs.DeleteKey("BoostedWinningsTimeLeft");
        PlayerPrefs.Save();
    }


    // Save the player's data to PlayerPrefs
    public void SaveData()
    {
        // Save player's money, VIP access, and position
        PlayerPrefs.SetInt("PlayerMoney", playerMoney);
        PlayerPrefs.SetInt("VIPAccess", hasVIPAccess ? 1 : 0);
        PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);

        // Save active items and their timers
        List<ItemSaveData> activeItems = new List<ItemSaveData>();

        if (isCostReductionActive)
        {
            activeItems.Add(new ItemSaveData
            {
                ItemName = "CostReduction",
                TimeLeft = costReductionTimeLeft
            });
        }

        if (isSpeedBoostActive)
        {
            activeItems.Add(new ItemSaveData
            {
                ItemName = "SpeedBoost",
                TimeLeft = speedBoostTimeLeft
            });
        }

        if (isBoostedWinningsActive)
        {
            activeItems.Add(new ItemSaveData
            {
                ItemName = "BoostedWinnings",
                TimeLeft = boostedWinningsTimeLeft
            });
        }

        // Save active items as JSON
        PlayerPrefs.SetString("ActiveItems", JsonUtility.ToJson(new ItemSaveDataWrapper { Items = activeItems }));

        PlayerPrefs.Save();
    }


    private void OnApplicationQuit()
    {
        // Save the data when the game is closed
        SaveData();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll(); // Wipe all saved data
        SceneManager.LoadScene("Main Menu"); // Return to the Main Menu scene
    }

        // Display the tutorial panel
    private void ShowTutorial()
    {
        tutorialPanel.SetActive(true); // Show the tutorial UI
        canMove = false; // Disable movement while the tutorial is active
    }

    // Hide the tutorial panel and mark it as seen
    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        canMove = true; // Re-enable player movement

        // Save the fact that the tutorial has been seen
        PlayerPrefs.SetInt("HasSeenTutorial", 1);
        PlayerPrefs.Save();
    }
}

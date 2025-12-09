// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class ChallengeManager : MonoBehaviour
// {
//     public static ChallengeManager Instance;

//     [Header("UI Elements")]
//     public GameObject challengeListUI;         // Panel to display challenges
//     public TMP_Text challengeDescriptionText;  // UI Text to display active challenge details
//     public Transform challengeListContent;     // Parent for dynamically created buttons
//     public Button challengeButtonPrefab;       // Prefab for challenge buttons

//     private List<Challenge> challenges = new List<Challenge>();
//     private Challenge activeChallenge;
//     private bool challengeActive;
//     private float challengeTimer;

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     private void Update()
//     {
//         if (challengeActive && activeChallenge != null)
//         {
//             challengeTimer -= Time.deltaTime;

//             // Check if the challenge time is up
//             if (challengeTimer <= 0)
//             {
//                 CompleteChallenge(false); // Challenge failed due to timeout
//             }
//         }
//     }

//     public void ShowChallenges()
//     {
//         challengeListUI.SetActive(true);
//         UpdateChallengeListUI();
//     }

//     private void UpdateChallengeListUI()
//     {
//         // Clear existing buttons
//         foreach (Transform child in challengeListContent)
//         {
//             Destroy(child.gameObject);
//         }

//         // Create a button for each challenge
//         foreach (Challenge challenge in challenges)
//         {
//             Button buttonInstance = Instantiate(challengeButtonPrefab, challengeListContent);
//             TMP_Text buttonText = buttonInstance.GetComponentInChildren<TMP_Text>();
//             buttonText.text = $"{challenge.title}\nCost: ${challenge.cost}\nReward: ${challenge.reward}";

//             // Set up the button to start this specific challenge
//             buttonInstance.onClick.AddListener(() => StartChallenge(challenge));
//         }
//     }

//     public void AddChallenge(Challenge challenge)
//     {
//         challenges.Add(challenge);
//     }

//     public void StartChallenge(Challenge challenge)
//     {
//         if (challengeActive)
//         {
//             Debug.Log("Finish the current challenge before starting a new one.");
//             return;
//         }

//         if (PlayerController.Instance.playerMoney >= challenge.cost)
//         {
//             PlayerController.Instance.SubtractMoney(challenge.cost);
//             activeChallenge = challenge;
//             challengeActive = true;
//             challengeTimer = challenge.timeLimit;
//             challengeListUI.SetActive(false);

//             challengeDescriptionText.text = $"{challenge.title}\n{challenge.description}\nTime Left: {challenge.timeLimit} seconds";
//         }
//         else
//         {
//             Debug.Log("Not enough money to start this challenge.");
//         }
//     }

//     public void CompleteChallenge(bool success)
//     {
//         if (success)
//         {
//             PlayerController.Instance.AddMoney(activeChallenge.reward);
//             Debug.Log("Challenge completed! Reward earned.");
//         }
//         else
//         {
//             Debug.Log("Challenge failed. Time ran out.");
//         }

//         // Reset the active challenge
//         activeChallenge = null;
//         challengeActive = false;
//         challengeTimer = 0;

//         ShowChallenges(); // Refresh the challenge list UI
//     }
// }

// using UnityEngine;

// public class ChallengeMachine : Interactable
// {
//     private bool isUnlocked = false;

//     public GameObject StoreUI; // The shop UI panel
//     public GameObject interactMsg;
//     public TMP_Text feedbackText; // Text element for feedback
//     public PlayerController player; // Reference to the playerController

//     public Button VIPButton; // Reference to the VIP purchase button

//     private bool isPlayerInRange = false; // Track if player is near the shop

//     // This function will be called when the player interacts with the machine
//     public override void Interact()
//     {
//         if (isUnlocked)
//         {
//             ChallengeManager.Instance.ShowChallenges();
//         }
//         else
//         {
//             Debug.Log("Complete all objectives to unlock the Challenge Machine.");
//         }
//     }

//     // Unlock the machine once all objectives are complete
//     public void UnlockMachine()
//     {
//         isUnlocked = true;
//     }
// }

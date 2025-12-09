// Objective.cs
using UnityEngine;

public class Objective
{
    public string Title { get; private set; }           // Title of the objective
    public string Description { get; private set; }     // Detailed description
    public int RewardAmount { get; private set; }       // Reward amount for completing the objective
    public bool IsComplete { get; private set; }        // Completion status

    public Objective(string title, string description, int rewardAmount)
    {
        Title = title;
        Description = description;
        RewardAmount = rewardAmount;
        IsComplete = false;
    }

    public void Complete()
    {
        IsComplete = true;
    }
}

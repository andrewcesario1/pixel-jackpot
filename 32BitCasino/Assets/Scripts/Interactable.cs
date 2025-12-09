using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // This is the method that will be overridden by child classes for specific interactions
    public abstract void Interact();
}

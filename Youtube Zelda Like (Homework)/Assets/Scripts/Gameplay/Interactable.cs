using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Context Clue")]
    public SignalSender contextSignal;

    // private
    private bool playerInRange;
    private bool isChest = false; // private variable for Chest Object

    public bool GetIsChest()
    {
        return isChest;
    }

    public void ChestSwitch() // Change isChest bool
    {
        if (!isChest)
            isChest = true;
        else
            isChest = false;
    }

    public bool GetPlayerInRange()
    {
        return playerInRange;
    }

    private void OnTriggerEnter2D(Collider2D collision) // change playerInRange to true if player enters collider
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            contextSignal.Raise(); // Raise ContextClue Signal
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // change playerInRange to false if player exits collider
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            contextSignal.Raise(); // Raise ContextClue Signal
            playerInRange = false;
        }
    }
}

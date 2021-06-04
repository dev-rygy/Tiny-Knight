using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Dialogue
{
    [Header("Chest")]
    public Item contents;
    public SignalSender raiseItem;
    public Collider2D triggerCollider;

    [Header("Player Inventory")]
    public Inventory playerInventory;

    // private
    private Animator myAnimator;
    private bool isLooted;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        dialogue = contents.itemDescription;
        ChestSwitch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && GetPlayerInRange() // These two if statemtns are for preventing context clue bug and dialogue bug with collider
                ) // Run before Chest is looted
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (!isLooted)
        {
            OpenChest();
        }
        else
        {
            ChestOpened();
        }
    }

    public void OpenChest()
    {
        promptDialogue();
        playerInventory.currentItem = contents;
        playerInventory.AddItem(contents);
        contextSignal.Raise();
        raiseItem.Raise();
        isLooted = true;
    }

    public void ChestOpened()
    {
        DisableDialogueBox();
        playerInventory.currentItem = null;
        raiseItem.Raise();
        triggerCollider.enabled = false;
    }
}

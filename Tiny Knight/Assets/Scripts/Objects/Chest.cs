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
    public BoolValue storedState; // Scriptable Object that holds a bool value

    [Header("Player Inventory")]
    public Inventory playerInventory;

    [Header("Coroutines")]
    public float openDelay = 0.5f;

    // private
    private Animator myAnimator;
    private bool isLooted;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        dialogue = contents.itemDescription;
        ChestSwitch();
        isLooted = storedState.runtimeValue;
        if(storedState.runtimeValue)
        {
            myAnimator.SetBool("isOpenStartOfScene", true);
            triggerCollider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!storedState.runtimeValue)
        {
            if (Input.GetButtonDown("Interact") && GetPlayerInRange()) // These two if statemtns are for preventing context clue bug and dialogue bug with collider
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
        if (!isLooted) // Open the Chest
        {
            playerInventory.currentItem = contents;
            playerInventory.AddItem(contents);
            contextSignal.Raise(); // Disable context clue before raising the item
            isLooted = true;
            raiseItem.Raise();
            StartCoroutine(OpenCo());
        }
        else // Chest has already been opened
        {
            contextSignal.Raise();
            raiseItem.Raise();
            DisableDialogueBox();
            playerInventory.currentItem = null;
            triggerCollider.enabled = false;
            storedState.runtimeValue = true;
        }
    }

    private IEnumerator OpenCo()
    {
        myAnimator.SetBool("isOpen", true);
        yield return new WaitForSeconds(openDelay);
        promptDialogue();
    }
}

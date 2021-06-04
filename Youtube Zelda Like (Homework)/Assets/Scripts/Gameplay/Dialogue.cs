using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : Interactable
{
    [Header("Dialogue")]
    [Tooltip("Dialogue Box object in the Canvas")]
    public GameObject dialogueBox;
    [Tooltip("Text object in the Canvas")]
    public Text dialogueText;
    [Tooltip("This string will be overwritten by Item if \"this\" is a Chest Object")]
    public string dialogue;
    public bool usingScriptableObj;
    [Tooltip("Use Text Box scriptable object for long text boxes")]
    public TextBox scriptableObj;

    //private
    private bool isOn = false;

    void Update()
    {
        if (!GetIsChest())
        {
            if (Input.GetButtonDown("Interact") && GetPlayerInRange()) // If the Player is in range and the button "Interact is pressed"
                promptDialogue();                                                           // Only go to promptDialogue if Object is not a Chest
            if (isOn && !GetPlayerInRange())
                DisableDialogueBox();
        }
    }

    public void promptDialogue() // Set specific dialogue box active
    {
        if (dialogueBox.activeInHierarchy)
        {
            DisableDialogueBox();
        }
        else
        {
            if (usingScriptableObj)
            {
                dialogueText.text = scriptableObj.GetText();
            }
            else
            {
                dialogueText.text = dialogue;
            }
            dialogueBox.SetActive(true);
            isOn = true;
        }
    }

    public void DisableDialogueBox()
    {
        Debug.Log("Disabled");
        dialogueBox.SetActive(false);
        isOn = false;
    }
}

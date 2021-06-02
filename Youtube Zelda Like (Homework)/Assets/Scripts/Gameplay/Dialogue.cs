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
    public string dialogue;
    public bool usingScriptableObj;
    [Tooltip("Use Text Box scriptable object for long text boxes")]
    public TextBox scriptableObj;

    [Header("Debug")]
    public bool debug = false;
    public string objectName;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && GetPlayerInRange()) // If the Player is in range and the button "Interact is pressed"
        {
            promptDialogue();
        }

        if (!GetPlayerInRange())
            DisableDialogueBox();
    }

    public void promptDialogue() // Set specific dialogue box active
    {
        if (dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(false);
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
        }
    }

    public void DisableDialogueBox()
    {
        dialogueBox.SetActive(false);
    }
}

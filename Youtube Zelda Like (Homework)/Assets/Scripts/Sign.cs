using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    [Header ("Caches")]
    public GameObject dialogueBox;
    public Text dialogText;
    public Dialogue scriptableObj;

    [Header("Text")]
    public string dialog;
    public bool usingScriptableObj;

    // private
    private bool playerInRange;

    void Update()
    {
        if(Input.GetButtonDown("Interact") && playerInRange)
        {
            promptDialogue();
        }
    }

    private void promptDialogue()
    {
        if (dialogueBox.activeInHierarchy)
        {
            dialogueBox.SetActive(false);
        }
        else
        {
            if(usingScriptableObj)
            {
                dialogText.text = scriptableObj.GetText();
            }
            else
            {
                dialogText.text = dialog;
            }
            dialogueBox.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}

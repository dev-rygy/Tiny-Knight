using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;

    public void Enable() // Will set Context clue "GameObject" to ON, through the ContextClue "script" on the player
    {
        contextClue.SetActive(true);
    }
    public void Disable() // Sets Context Clue GameObject to OFF
    {
        contextClue.SetActive(false);
    }
}

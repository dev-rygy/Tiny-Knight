using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Box")]
public class TextBox : ScriptableObject
{
    [TextArea(14, 10)] [SerializeField] string DialogueText; // Creates a text field that holds a string

    public string GetText() //method for other objects to call that need a fairly long string
    {
        return DialogueText; // return the string
    }
}

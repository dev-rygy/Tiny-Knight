using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialoge")]
public class Dialogue : ScriptableObject
{
    [TextArea(14,10)] [SerializeField] string DialogueText;
}

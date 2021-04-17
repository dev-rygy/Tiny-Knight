using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Breakable")) // If Tag is "breakable"
        {
            StartCoroutine(collision.GetComponent<Breakable>().BreakCo()); // start the coroutine to break
        }
    }
}

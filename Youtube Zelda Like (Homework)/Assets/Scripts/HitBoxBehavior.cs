using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{
    [Header("Properties")]
    public bool canBreakObjects = false;

    [Header("Knockback")]
    public float thrust = 2f; // The amount of thrust applied to the object collided w/
    public float knocktime = 0.2f; // The amount of time the object is in stagger
    public float recoverDelay = 0f;

    private void OnTriggerEnter2D(Collider2D targetCollider) // When the target's collider is hit by the attacker's collider
    {
        BreakObject(targetCollider);
        Knockback(targetCollider);
    }

    private void BreakObject(Collider2D targetCollider) // When the attackers collider comes in contact with object on layer "Breakable"
    {
        if (canBreakObjects == true // Can the attacking object break inanimate objects?
            && targetCollider.gameObject.CompareTag("Breakable")) // Is the object of type "Breakable?
        {
                StartCoroutine(targetCollider.GetComponent<Breakable>().BreakCo()); // start the coroutine to break
        }
    }

    private void Knockback(Collider2D targetCollider) // When the attacker's collider comes in contact with object of type "Enemy" / "Player"
    {
        if (targetCollider.gameObject.CompareTag("Enemy") || targetCollider.gameObject.CompareTag("Player")) // If collision Object is Player or Enemy
        {
            Rigidbody2D targetCollision = targetCollider.GetComponent<Rigidbody2D>();

            Vector2 targetOffset = targetCollision.transform.position - transform.position; // help determine direction

            if (targetCollision != null && targetCollision.gameObject.CompareTag("Enemy")
                   && targetCollision.GetComponent<Enemy>().currentState != EnemyState.stagger) // Enemy collision
            {
                targetCollision.AddForce(KnockDirection(targetOffset), ForceMode2D.Impulse); // Force and direction applied to collision
                targetCollision.GetComponent<Enemy>().EnemyKnockCo(targetCollision, knocktime, recoverDelay); // Start KnockCo
            }
            if (targetCollision != null && targetCollision.gameObject.CompareTag("Player")) // Player collision
            {
                targetCollision.AddForce(KnockDirection(targetOffset), ForceMode2D.Impulse); // Force and direction applied to collision
                targetCollision.GetComponent<Player>().StartPlayerKnockCo(knocktime, recoverDelay); // Start KnockCo
            }
        }
    }

    private Vector2 KnockDirection(Vector2 targetOffset) // Direction of knockback
    {
        if (Mathf.Abs(targetOffset.x) > Mathf.Abs(targetOffset.y)) // if otherCollision positioned right/left 
        {
            if(targetOffset.x > 0) // if right
            {
                targetOffset = new Vector2(1f, 0f);
            }
            else // if left
            {
                targetOffset = new Vector2(-1f, 0f);
            }
        }
        if (Mathf.Abs(targetOffset.x) < Mathf.Abs(targetOffset.y)) // if otherCollision positioned up/down 
        {
            if (targetOffset.y > 0) // if Up
            {
                targetOffset = new Vector2(0f, 1f);
            }
            else // if down
            {
                targetOffset = new Vector2(0f, -1f);
            }
        }
        targetOffset = targetOffset * thrust; // Implement thrust amt.
        return targetOffset;
    }
}

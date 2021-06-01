using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{
    [Header("Properties")]
    public string hitboxName;
    public bool canBreakObjects = false;

    [Header("Knockback")]
    public float thrust = 2f; // The amount of thrust applied to the object collided w/
    public float knocktime = 0.2f; // The amount of time the object is in stagger
    public float recoverDelay = 0f;

    [Header("Damage")]
    public float damage = 0;

    private void OnTriggerEnter2D(Collider2D targetCollider) // When the target's collider is hit by the attacker's collider
    {
        BreakObject(targetCollider);
        KnockbackAndDamage(targetCollider);
    }

    private void BreakObject(Collider2D targetCollider) // When the attackers collider comes in contact with object on layer "Breakable"
    {
        if (canBreakObjects == true // Can the attacking object break inanimate objects?
            && targetCollider.gameObject.CompareTag("Breakable")) // Is the object of type "Breakable? Is the collider on the object a trigger?
        {
                StartCoroutine(targetCollider.GetComponent<Breakable>().BreakCo()); // start the coroutine to break
        }
    }

    private void KnockbackAndDamage(Collider2D targetCollider) // When the attacker's collider comes in contact with object of type "Enemy" / "Player"
    {
        if (targetCollider.gameObject.CompareTag("Enemy") || targetCollider.gameObject.CompareTag("Player")
                && targetCollider.isTrigger) // If collision Object is Player or Enemy
        {
            Rigidbody2D targetRigidbody2D = targetCollider.GetComponent<Rigidbody2D>();

            Vector2 targetOffset = targetRigidbody2D.transform.position - transform.position; // help determine direction by finding the difference
                                                                                                // of the targets transform - who ever has this scrip's transform
            if (targetRigidbody2D != null && targetRigidbody2D.gameObject.CompareTag("Enemy")
                   && targetRigidbody2D.GetComponent<Enemy>().currentState != EnemyState.stagger
                   && targetRigidbody2D.GetComponent<Enemy>().currentState != EnemyState.dead) // Enemy collision
            {
                Debug.Log("Player did " + damage + " damage with " + hitboxName + "!");
                targetRigidbody2D.AddForce(KnockDirection(targetOffset), ForceMode2D.Impulse); // Force and direction applied to collision
                targetRigidbody2D.GetComponent<Enemy>().Hit(targetRigidbody2D, knocktime, recoverDelay, damage); // Start KnockCo and take dmg
            }

            if (targetRigidbody2D != null && targetRigidbody2D.gameObject.CompareTag("Player")
                    && targetRigidbody2D.GetComponent<Player>().currentState != PlayerState.stagger
                    && targetRigidbody2D.GetComponent<Player>().currentState != PlayerState.dead) // Player collision
            {
                Debug.Log(this.GetComponent<Enemy>().enemyName + " did " + damage + " damage with " + hitboxName + "!");
                targetRigidbody2D.AddForce(KnockDirection(targetOffset), ForceMode2D.Impulse); // Force and direction applied to collision
                targetRigidbody2D.GetComponent<Player>().Hit(knocktime, recoverDelay, damage); // Start KnockCo and take dmg
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

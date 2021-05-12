using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{
    [Header("Knockback")]
    public float thrust = 2f; // The amount of thrust applied to the object collided w/
    public float knocktime = 0.2f; // The amount of time the object is in stagger
    public float recoverDelay = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable")) // If Tag is "breakable"
        {
            StartCoroutine(collision.GetComponent<Breakable>().BreakCo()); // start the coroutine to break
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player")) // If collision Object is Player or Enemy
        {
            Rigidbody2D otherRigidbody2D = collision.GetComponent<Rigidbody2D>();

            Vector2 otherOffset = otherRigidbody2D.transform.position - transform.position; // help determine direction

            if (otherRigidbody2D != null && otherRigidbody2D.gameObject.CompareTag("Enemy")
                   && otherRigidbody2D.GetComponent<Enemy>().currentState != EnemyState.stagger) // Enemy collision
            {
                otherRigidbody2D.AddForce(KnockDirection(otherOffset), ForceMode2D.Impulse); // Force and direction applied to collision
                otherRigidbody2D.GetComponent<Enemy>().EnemyKnockCo(otherRigidbody2D, knocktime, recoverDelay);
            }
            if (otherRigidbody2D != null && otherRigidbody2D.gameObject.CompareTag("Player")) // Player collision
            {
                otherRigidbody2D.AddForce(KnockDirection(otherOffset), ForceMode2D.Impulse); // Force and direction applied to collision
                otherRigidbody2D.GetComponent<Player>().PlayerKnockCo(knocktime, recoverDelay);
            }
        }
    }

    private Vector2 KnockDirection(Vector2 otherOffset)
    {
        if (Mathf.Abs(otherOffset.x) > Mathf.Abs(otherOffset.y)) // if otherCollision positioned right/left 
        {
            if(otherOffset.x > 0) // if right
            {
                otherOffset = new Vector2(1f, 0f);
            }
            else // if left
            {
                otherOffset = new Vector2(-1f, 0f);
            }
        }
        if (Mathf.Abs(otherOffset.x) < Mathf.Abs(otherOffset.y)) // if otherCollision positioned up/down 
        {
            if (otherOffset.y > 0) // if Up
            {
                otherOffset = new Vector2(0f, 1f);
            }
            else // if down
            {
                otherOffset = new Vector2(0f, -1f);
            }
        }
        otherOffset = otherOffset * thrust;
        return otherOffset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    wakingUp,
    sleeping,
    dead
}

public class Enemy : MonoBehaviour
{
    [Header("Enemy State")]
    public EnemyState currentState;
    public bool invulnerable = false;

    [Header("Enemy Stats")]
    public FloatReference maxHealth;
    public float health;
    public float moveSpeed = 2f;

    [Header("Enemy Properties")]
    public string enemyName;

    private void Awake()
    {
        health = maxHealth.Value();
    }

    public void ChangeState(EnemyState newState) // Change EnemyState
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void Hit(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay, float damage) // Start KnockCo and take damage
    {
        if (invulnerable != true)
            TakeDamage(damage);
        if (health > 0f)
            StartCoroutine(KnockCo(myRigidbody2D, knocktime, recoverDelay, damage));
    }

    private void TakeDamage(float damage) // Take dmg and update hp
    {
        if (currentState != EnemyState.stagger && health > 0f)
        {
            health -= damage;
        }
        if (health <= 0f) // kill off the Enemy once health reaches 0
        {
            ChangeState(EnemyState.dead);
            this.gameObject.SetActive(false); // will be put into a coroutine later
            Debug.Log(enemyName + " has died.");
        }
        invulnerable = true;
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay, float damage) // Enemy KnockCo
    {
        if (myRigidbody2D != null && currentState != EnemyState.stagger)
        {
            currentState = EnemyState.stagger;
            FindObjectOfType<Log>().isHurt();
            yield return new WaitForSeconds(knocktime);
            myRigidbody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(recoverDelay);
            invulnerable = false;
            currentState = EnemyState.idle;
        }
    }
}

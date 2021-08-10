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
    private bool invulnerable = false;

    [Header("Enemy Stats")]
    public FloatReference maxHealth;
    private float health;
    public float moveSpeed = 2f;

    [Header("Enemy Properties")]
    public string enemyName;
    public GameObject deathEffect;

    [Header("Debug")]
    public bool debugModeEnabler = false;

    private void Awake()
    {
        health = maxHealth.GetValue();
    }

    public void ChangeState(EnemyState newState) // Change EnemyState
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void Hit(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay, float damage, Vector2 knockDirection) // Start KnockCo and take damage
    {
        if (invulnerable != true)
            TakeDamage(damage);
        if (health > 0f)
            StartCoroutine(KnockCo(myRigidbody2D, knocktime, recoverDelay, knockDirection));
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
            DeathEffect();
            this.gameObject.SetActive(false); // will be put into a coroutine later
            DebugMode(1);
        }
        invulnerable = true;
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // Destroy Death Effect Game Object after 1 second
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay, Vector2 knockDirection) // Enemy KnockCo
    {
        if (myRigidbody2D != null && currentState != EnemyState.stagger)
        {
            ChangeState(EnemyState.stagger);
            myRigidbody2D.AddForce(knockDirection, ForceMode2D.Impulse); // Force and direction applied to collision
            myRigidbody2D.GetComponent<Log>().isHurt();
            yield return new WaitForSeconds(knocktime);
            myRigidbody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(recoverDelay);
            invulnerable = false;
            currentState = EnemyState.idle;
        }
    }

    private void DebugMode(int debugCode)
    {
        if (debugModeEnabler)
        {
            switch (debugCode)
            {
                case 1:
                    Debug.Log(enemyName + " has died.");
                    break;
            }
        }
    }
}

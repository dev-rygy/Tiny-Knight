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
    sleeping
}

public class Enemy : MonoBehaviour
{
    [Header("Enemy State")]
    public EnemyState currentState;

    [Header("Enemy Properties")]
    public string enemyName;
    public int health;
    public int baseAttack;
    public float moveSpeed;

    public void ChangeState(EnemyState newState) // Change EnemyState
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void EnemyKnockCo(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay) // Start KnockCo
    {
        StartCoroutine(KnockCo(myRigidbody2D, knocktime, recoverDelay));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay) // Enemy KnockCo
    {
        if (myRigidbody2D != null && currentState != EnemyState.stagger)
        {
            currentState = EnemyState.stagger;
            FindObjectOfType<Log>().isHurt();
            yield return new WaitForSeconds(knocktime);
            myRigidbody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(recoverDelay);
            currentState = EnemyState.idle;
        }
    }
}

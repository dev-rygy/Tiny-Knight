using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    public void EnemyKnockCo(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay)
    {
        StartCoroutine(KnockCo(myRigidbody2D, knocktime, recoverDelay));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody2D, float knocktime, float recoverDelay)
    {
        if (myRigidbody2D != null && currentState != EnemyState.stagger)
        {
            currentState = EnemyState.stagger;
            yield return new WaitForSeconds(knocktime);
            myRigidbody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(recoverDelay);
            currentState = EnemyState.idle;
        }
    }
}

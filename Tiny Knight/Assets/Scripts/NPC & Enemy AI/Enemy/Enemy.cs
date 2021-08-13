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
    public bool PersuingEnemy = false;
    public bool PathingEnemy = false;
    public bool SleepingEnemy = false;

    [Header("Target and Persue")]
    public Transform target;
    public float chaseRadius = 5f;
    public float attackRadius = 0.5f;
    public bool hasHomePosition = true;
    public Vector2 homePosition;

    [Header("Pathing")]
    public Transform[] path; // Multiple Transforms in an array, the Log will move from one Transform to the next respectively
    public bool reversePathing = false; // Instead of the currentPoint resetting at the end of the path, the currentPoint will reverse order till the first
    public int currentPoint; // an int to track the point in the path array
    public float roundingDistance = 0.1f; // The distance that the Log needs to be from the currentPoint's position to go to the next point

    [Header("Effects")]
    public GameObject deathEffect;

    [Header("Coroutines")]
    public float wakeUpdelay = 0.7f;

    [Header("Debug")]
    public bool debugModeEnabler = false;

    // public cache
    [HideInInspector] public Rigidbody2D myRidgidBody; // made public for PatrolLog
    [HideInInspector] public Animator myAnimator; // made public for PatrolLog

    // private
    private bool reverse = false;
    private float sleepRounding = 0.2f;

    private void Awake()
    {
        health = maxHealth.GetValue();
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        homePosition = transform.position;
    }

    // Change State
    public void ChangeState(EnemyState newState) // Change EnemyState
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    // Stat Changes
        // Incoming Damage
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

    // AI
        // Target and Persue
    public virtual bool CheckDistanceOfTarget() // Wake up / move / fall asleep; made virtual so that PatrolLog can override with it's own CheckDistance
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius // Check if the target's distance is close enough
                && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            if (SleepingEnemy && currentState == EnemyState.sleeping)
                WakeUp(); // Wake up the Log
            if (PersuingEnemy)
            {
                MoveToTarget(); // Move towards the target
            }
            return true;
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius)// Target out of range = fall back asleep
        {
            Debug.Log("out");
            if (PathingEnemy && Vector2.Distance(target.position, transform.position) > chaseRadius) // Target out of range = move to next pathing point
            {
                MoveToPoint(); // Move to the currentPoint
            }
            else if (SleepingEnemy && Vector2.Distance(transform.position, homePosition) <= sleepRounding
                   && currentState == EnemyState.walk)
                GoToSleep();
            else if (hasHomePosition && currentState != EnemyState.stagger && currentState != EnemyState.sleeping
                && myAnimator.GetBool("isAwake"))
                MoveToHomePosition();
            return false;
        }
    }

    private void MoveToTarget() // Move towards the target if not staggered or sleeping
    {
        if (currentState != EnemyState.stagger && currentState != EnemyState.sleeping
                && myAnimator.GetBool("isAwake"))
        {
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnimDirection(temp - Vector3Extension.AsVector2(transform.position));
            myRidgidBody.MovePosition(temp);
        }
    }

        // Home Position
    public void MoveToHomePosition() // The log will to back to it's home position and sleep when the player is out of range
    {
        ChangeState(EnemyState.walk);
        Vector2 temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
        ChangeAnimDirection(temp - Vector3Extension.AsVector2(transform.position));
        myRidgidBody.MovePosition(temp);
    }

        // Pathing
    private void MoveToPoint()
    {
        if (Vector2.Distance(transform.position, path[currentPoint].position) > roundingDistance) // if the difference of the Log and it's next point is greater
        {                                                                                               // than the minimum distance set in rounding distance
            Vector2 temp = Vector2.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
            ChangeAnimDirection(temp - Vector3Extension.AsVector2(transform.position));
            myRidgidBody.MovePosition(temp);
        }
        else
        {
            if (reversePathing)
                ReversePathing();
            else
                ChangeGoal();
        }
    }

    private void ChangeGoal() // Change the Log's current goal to the next
    {
        if (currentPoint == path.Length - 1) // If currentPoint == end of the path then reset the path
            currentPoint = 0;
        else // increment point and go to next
            currentPoint++;
    }

    private void ReversePathing() // Reverses the pathing of the enemy instead of restarting
    {
        if (currentPoint == path.Length - 1)
        {
            reverse = true;
            currentPoint--;
        }
        else if (currentPoint == 0)
        {
            reverse = false;
            currentPoint++;
        }
        else if (reverse)
            currentPoint--;
        else
            currentPoint++;
    }

        // Wake Up and Sleep
    public void WakeUp() // Wake up if sleeping
    {
        StartCoroutine(WakeUpCo());
    }

    public void GoToSleep()
    {
        ChangeState(EnemyState.sleeping);
        myAnimator.SetBool("isAwake", false);
    }

    //Animation
    public void ChangeAnimDirection(Vector2 direction) // Change the direction of moveX and moveY
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0) // moving right
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0) // moving left
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0) // moving up
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0) // moving down
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void SetAnimFloat(Vector2 setVector) // set float for moveX and moveY
    {
        myAnimator.SetFloat("moveX", setVector.x);
        myAnimator.SetFloat("moveY", setVector.y);
    }

    public void isHurt() // hurt animation if staggered from Enemy script
    {
        myAnimator.SetTrigger("hurtTrigger");
    }

    // Effects
    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f); // Destroy Death Effect Game Object after 1 second
        }
    }

    // Coroutines
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

    private IEnumerator WakeUpCo() // Wake up coroutine
    {
        myAnimator.SetBool("isAwake", true);
        yield return new WaitForSeconds(wakeUpdelay);
        ChangeState(EnemyState.idle);
    }

    // Debug
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

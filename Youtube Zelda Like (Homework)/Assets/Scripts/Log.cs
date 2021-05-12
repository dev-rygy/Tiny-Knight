using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    [Header("Movement")]
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;

    [Header("Coroutines")]
    public float wakeUpdelay = 2f;

    // private cache
    private Rigidbody2D myRidgidBody;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.sleeping;
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform; // plug in the player's position in the world
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance() // Wake up / move / fall asleep
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius // Check if the target's distance is close enough
                && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            WakeUp(); // Wake up the Log
            MoveToTarget(); // Move towards the target
        }
        else // Target out of range = fall back asleep
        {
            myAnimator.SetBool("isAwake", false);
            ChangeState(EnemyState.sleeping);
        }
    }

    private void WakeUp() // Wake up if sleeping
    {
        if (currentState == EnemyState.sleeping)
            StartCoroutine(WakeUpCo());
    }

    private void MoveToTarget() // Move towards the target if not staggered or sleeping
    {
        if (currentState != EnemyState.stagger && currentState != EnemyState.sleeping)
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            myRidgidBody.MovePosition(temp);
        }
    }

    private IEnumerator WakeUpCo() // Wake up coroutine
    {
        myAnimator.SetBool("isAwake", true);
        yield return new WaitForSeconds(wakeUpdelay);
        ChangeState(EnemyState.walk);
    }

    public void isHurt() // hurt animation if staggered from Enemy script
    {
        myAnimator.SetTrigger("hurtTrigger");
    }
}

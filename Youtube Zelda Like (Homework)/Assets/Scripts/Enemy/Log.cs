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
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius)// Target out of range = fall back asleep
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
            ChangeState(EnemyState.walk);
            Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnimDirection(temp - Vector3Extension.AsVector2(transform.position));
            myRidgidBody.MovePosition(temp);
        }
    }

    private void ChangeAnimDirection(Vector2 direction) // Change the direction of moveX and moveY
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0) // moving right
            {
                SetAnimFloat(Vector2.right);
            }
            else if(direction.x < 0) // moving left
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

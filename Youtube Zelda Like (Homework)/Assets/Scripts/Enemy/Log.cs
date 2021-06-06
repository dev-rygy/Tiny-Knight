using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    [Header("Movement")]
    public Transform target;
    public float chaseRadius = 5f;
    public float attackRadius = 0.5f;
    public Transform homePosition;

    [Header("Coroutines")]
    public float wakeUpdelay = 0.7f;

    // public cache
    [HideInInspector] public Rigidbody2D myRidgidBody; // made public for PatrolLog
    [HideInInspector] public Animator myAnimator; // made public for PatrolLog
    // private cache


    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.sleeping;
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform; // plug in the player's position in the world
        myAnimator.SetBool("isAwake", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance() // Wake up / move / fall asleep; made virtual so that PatrolLog can override with it's own CheckDistance
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius // Check if the target's distance is close enough
                && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.sleeping)
                WakeUp(); // Wake up the Log
            MoveToTarget(); // Move towards the target
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius)// Target out of range = fall back asleep
        {
            ChangeState(EnemyState.sleeping);
            myAnimator.SetBool("isAwake", false);
        }
    }

    public void WakeUp() // Wake up if sleeping
    {
            StartCoroutine(WakeUpCo());
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

    public void ChangeAnimDirection(Vector2 direction) // Change the direction of moveX and moveY
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
        ChangeState(EnemyState.idle);
    }

    public void isHurt() // hurt animation if staggered from Enemy script
    {
        myAnimator.SetTrigger("hurtTrigger");
    }
}

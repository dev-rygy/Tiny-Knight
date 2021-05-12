using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
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

    void CheckDistance()
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius
                && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.sleeping)
                StartCoroutine(WakeUpCo());

            if (currentState != EnemyState.stagger && currentState != EnemyState.sleeping)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                myRidgidBody.MovePosition(temp);
            }
        }
        else
        {
            myAnimator.SetBool("isAwake", false);
            ChangeState(EnemyState.sleeping);
        }
    }

    private IEnumerator WakeUpCo()
    {
        myAnimator.SetBool("isAwake", true);
        yield return new WaitForSeconds(wakeUpdelay);
        ChangeState(EnemyState.walk);
    }

    public void isHurt()
    {
        myAnimator.SetTrigger("hurtTrigger");
    }
}

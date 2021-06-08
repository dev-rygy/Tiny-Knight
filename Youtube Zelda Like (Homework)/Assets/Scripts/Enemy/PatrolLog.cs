using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    [Header("Pathing")]
    public Transform[] path; // Multiple Transforms in an array, the Log will move from one Transform to the next respectively
    public bool reversePathing = false; // Instead of the currentPoint resetting at the end of the path, the currentPoint will reverse order till the first
    public int currentPoint; // an int to track the point in the path array
    public float roundingDistance = 0.1f; // The distance that the Log needs to be from the currentPoint's position to go to the next point

    // private
    private bool reverse = false;

    void Start()
    {
        currentState = EnemyState.walk;
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform; // plug in the player's position in the world
        myAnimator.SetBool("isAwake", true);
    }

    public override void CheckDistance() // This CheckDistance overrides the one in Log
    {
        if (Vector2.Distance(target.position, transform.position) <= chaseRadius // Check if the target's distance is close enough
                && Vector2.Distance(target.position, transform.position) > attackRadius) // if it is then chase the target
        {
            if (currentState == EnemyState.sleeping)
                WakeUp(); // Wake up the Log
            MoveToTarget(); // Move towards the target
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRadius) // Target out of range = move to next pathing point
        {
            MoveToPoint(); // Move to the currentPoint
        }
    }

    private void MoveToTarget() // Move towards the target if not staggered or sleeping
    {
        if (currentState != EnemyState.stagger && currentState != EnemyState.sleeping
                && myAnimator.GetBool("isAwake"))
        {
            Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnimDirection(temp - Vector3Extension.AsVector2(transform.position));
            myRidgidBody.MovePosition(temp);
        }
    }

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Enemy
{
    void Start()
    {
        currentState = EnemyState.walk;
        myAnimator.SetBool("isAwake", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState != EnemyState.stagger)
        {
            if (CheckIfTargetInBoundary() && CheckDistanceOfTarget() 
                && currentState != EnemyState.stagger)
            {
                MoveToTarget();
            }
            else
            {
                MoveToPoint();
            }
        }
    }
}

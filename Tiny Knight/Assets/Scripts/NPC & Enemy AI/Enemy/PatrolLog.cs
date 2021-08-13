using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    void Start()
    {
        PersuingEnemy = true;
        PathingEnemy = true;
        currentState = EnemyState.walk;
        myAnimator.SetBool("isAwake", true);
    }

    void FixedUpdate()
    {
        CheckDistanceOfTarget();
    }
}

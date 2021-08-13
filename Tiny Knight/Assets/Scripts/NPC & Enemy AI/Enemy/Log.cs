using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        PersuingEnemy = true;
        SleepingEnemy = true;
        currentState = EnemyState.sleeping;
        myAnimator.SetBool("isAwake", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistanceOfTarget();
    }
}

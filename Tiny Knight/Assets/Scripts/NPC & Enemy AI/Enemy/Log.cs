using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.sleeping;
        myAnimator.SetBool("isAwake", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState != EnemyState.stagger)
        {
            if (CheckDistanceOfTarget() && currentState != EnemyState.stagger)
            {
                if (currentState == EnemyState.sleeping)
                    WakeUp();
                MoveToTarget();
            }
            else if (hasHomePosition && Vector2.Distance(transform.position, homePosition) > sleepRounding)
            {
                MoveToHomePosition();
            }
            else
            {
                GoToSleep();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.sleeping;
        target = GameObject.FindWithTag("Player").transform; // plug in the player's position in the world
        myAnimator.SetBool("isAwake", false);
        homePosition = transform.position;
        SleepingEnemy = true;
        PersuingEnemy = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckTargetDistance();
    }
}

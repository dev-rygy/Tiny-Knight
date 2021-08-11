using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Enemy
{
    void Start()
    {
        currentState = EnemyState.walk;
        target = GameObject.FindWithTag("Player").transform; // plug in the player's position in the world
        PersuingEnemy = true;
        PathingEnemy = true;
    }

    void FixedUpdate()
    {
        CheckTargetDistance();
        DebugMode(2);
    }
}

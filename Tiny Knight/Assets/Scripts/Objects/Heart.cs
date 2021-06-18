using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatVariable playerHealth;
    public FloatVariable heartContainers;
    public float amountToIncrease;

    public void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if (collisionObj.CompareTag("Player") && !collisionObj.isTrigger)
        {
            playerHealth.runtimeValue += amountToIncrease;
            if (playerHealth.value > heartContainers.runtimeValue * 2f)
                playerHealth.value = heartContainers.runtimeValue * 2f;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public Inventory playerInventory;
    public int value = 1;

    void Start()
    {
        powerUpSignal.Raise();
    }

    public void OnTriggerEnter2D(Collider2D collisionObj)
    {
        if (collisionObj.CompareTag("Player") && !collisionObj.isTrigger)
        {
            playerInventory.numberOfCoins += value;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}

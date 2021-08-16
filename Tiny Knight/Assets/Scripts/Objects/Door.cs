using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
}

public class Door : Interactable
{
    [Header("DoorType")]
    public DoorType thisDoorType;

    [Header("Public Cache")]
    public Inventory playerInventory;

    // private
    // private bool isOpen = false;

    private void Update()
    {
        if (thisDoorType == DoorType.key)
            KeyDoor();
    }

    // Key Door
    private void KeyDoor()
    {
        if (Input.GetButtonDown("Interact") && GetPlayerInRange())
        {
            if (CheckForKey())
                Open();
        }
    }

    private bool CheckForKey()
    {
        bool hasKey = false;
        if (playerInventory.numberOfKeys > 0)
        {
            // Remove Key
            playerInventory.numberOfKeys--;
            return hasKey = true;
        }
        else
            return hasKey;
    }

    public void Open()
    {
        this.gameObject.SetActive(false);
    }
}

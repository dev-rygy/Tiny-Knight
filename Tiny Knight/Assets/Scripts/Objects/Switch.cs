using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool activated;
    public BoolValue storedValue;
    public Sprite activatedSprite;
    private SpriteRenderer mySprite;
    public Door linkedDoor;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        activated = storedValue.runtimeValue;
        if (activated)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        activated = true;
        storedValue.runtimeValue = activated;
        linkedDoor.Open();
        mySprite.sprite = activatedSprite;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}

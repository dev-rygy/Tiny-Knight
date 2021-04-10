using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Velocity")]
    public float walkSpeed = 5f; // Player walk speed

    [Header("Cached References")]

    // Private Chached References
    private Rigidbody2D myRigedBody; // cashed reference
    private Vector2 changeInVelocity;

    // Start is called before the first frame update
    void Start()
    {
        myRigedBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            MoveCharacter(); // Move the character
    }

    void MoveCharacter()
    {
        changeInVelocity = Vector2.zero;
        changeInVelocity.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * walkSpeed; // (1, 0 or -1) * walkspeed * seconds from the last frame
        changeInVelocity.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * walkSpeed; // (1, 0 or -1) * walkspeed * seconds from the last frame

        if (changeInVelocity != Vector2.zero) // If the Player has movement input
        {
            transform.Translate(new Vector2(changeInVelocity.x, changeInVelocity.y));
        }
        /*
         * Alternate Movecharacter Algorithm
            changeInVelocity = Vector2.zero; //Set the player's movement to 0 every frame
            changeInVelocity.x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * walkSpeed; // (1, 0 or -1) * walkspeed
            changeInVelocity.y = Input.GetAxisRaw("Vertical") * Time.deltaTime * walkSpeed; // (1, 0 or -1) * walkspeed
            
            myRigedBody.velocity = changeInVelocity; //Set the velocity of the Player to the change in velocity
        */
    }
}

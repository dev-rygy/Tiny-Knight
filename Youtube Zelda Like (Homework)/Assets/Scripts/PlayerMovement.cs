using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Velocity")]
    public float walkSpeed = 5f; // Player walk speed

    [Header("Cached References")]

    // Private Chached References
    private Rigidbody2D myRigedbody; // cached Rigidbody2D reference
    private Vector2 changeInVelocity;
    private Animator myAnimator; // chached Animator Reference

    // Start is called before the first frame update
    void Start()
    {
        myRigedbody = GetComponent<Rigidbody2D>(); // Set myRigidbody to the object's Rigidbody component
        myAnimator = GetComponent<Animator>(); // Set myAnimator to the object's Animator component
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter(); // Move the character
    }

    void MoveCharacter()
    {
        //changeInVelocity = Vector2.zero;
        changeInVelocity.x = Input.GetAxisRaw("Horizontal") * walkSpeed; // (1, 0 or -1) * walkspeed * seconds from the last frame
        changeInVelocity.y = Input.GetAxisRaw("Vertical") * walkSpeed; // (1, 0 or -1) * walkspeed * seconds from the last frame
        myRigedbody.velocity = changeInVelocity;

        bool playerIsMoving = Mathf.Abs(myRigedbody.velocity.x) > Mathf.Epsilon
           || Mathf.Abs(myRigedbody.velocity.y) > Mathf.Epsilon;
        if (playerIsMoving)
        {
            myAnimator.SetFloat("moveX", changeInVelocity.x); // change idle animation relative to where the Player is facing
            myAnimator.SetFloat("moveY", changeInVelocity.y); // change idle animation relative to where the Player is facing
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }

        /*
        if (changeInVelocity != Vector2.zero) // If the Player has movement input; vital for the player to remain facing the direction of the last input
        {
            transform.Translate(new Vector2(changeInVelocity.x, changeInVelocity.y)); // move the Player relative to the input
            myAnimator.SetFloat("moveX", changeInVelocity.x); // change idle animation relative to where the Player is facing
            myAnimator.SetFloat("moveY", changeInVelocity.y); // change idle animation relative to where the Player is facing
            myAnimator.SetBool("isWalking", true);
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    interact,
    attack
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Velocity")]
    public float walkSpeed = 5f; // Player walk speed

    [Header("Player State")]
    public PlayerState currentState;

    [Header("Coroutines")]
    public float attackDelay = 0.25f;

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
        currentState = PlayerState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != PlayerState.attack)
        {
            MoveCharacter(); // Move the character every frame
            Attack();
        }
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
            currentState = PlayerState.walk;
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
            currentState = PlayerState.idle;
        }
    }

    void Attack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo()
    {
        myRigedbody.velocity = Vector2.zero;
        currentState = PlayerState.attack;
        myAnimator.SetBool("isAttacking", true);
        yield return null;
        myAnimator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackDelay);
        currentState = PlayerState.idle;
    }
}

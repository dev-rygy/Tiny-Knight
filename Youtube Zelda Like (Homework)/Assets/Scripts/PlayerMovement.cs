using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    interact,
    attack,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Velocity")]
    public float walkSpeed = 5f; // Player walk speed

    [Header("Player State")]
    public PlayerState currentState; // current PlayerState

    [Header("Coroutines")]
    public float attackDelay = 0.25f; // Attack freeze delay

    [Header("Cached References")]

    // Private Chached References
    private Rigidbody2D myRigidbody2D; // cached Rigidbody2D reference
    private Vector2 changeInVelocity; // current velocity direction and magnitude on the player
    private Animator myAnimator; // chached Animator Reference

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>(); // Set myRigidbody to the object's Rigidbody component
        myAnimator = GetComponent<Animator>(); // Set myAnimator to the object's Animator component
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1);
        currentState = PlayerState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != PlayerState.attack && currentState != PlayerState.stagger) // If Player is not attacking
        {
            MoveCharacter(); // Move the character every frame
            Attack(); // Attack on button press
        }
    }

    void MoveCharacter()
    {
        changeInVelocity.Normalize(); // Makes diagnal speed slightly slower
        changeInVelocity.x = Input.GetAxisRaw("Horizontal") * walkSpeed; // (1, 0 or -1) * walkspeed * seconds from the last frame
        changeInVelocity.y = Input.GetAxisRaw("Vertical") * walkSpeed; // (1, 0 or -1) * walkspeed * seconds from the last frame
        myRigidbody2D.velocity = changeInVelocity;

        bool playerIsMoving = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon
           || Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        if (playerIsMoving) // If velocity magnitude > Epsilon
        {
            if (Mathf.Abs(changeInVelocity.x) > Mathf.Abs(changeInVelocity.y))
            {
                myAnimator.SetFloat("moveX", changeInVelocity.x); // change idle animation relative to where the Player is facing
                myAnimator.SetFloat("moveY", 0);
            }
            else
            {
                myAnimator.SetFloat("moveX", 0);
                myAnimator.SetFloat("moveY", changeInVelocity.y); // change idle animation relative to where the Player is facing
            }
            myAnimator.SetBool("isWalking", true);
            currentState = PlayerState.walk;
        }
        else
        {
            myAnimator.SetBool("isWalking", false);
            currentState = PlayerState.idle; // change to idle if player is not walking
        }
    }

    void Attack() // Runs attack Co if Player presses attack button
    {
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.attack
             && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
    }

    public void PlayerKnockCo(float knocktime, float recoverDelay)
    {
        StartCoroutine(KnockCo(knocktime, recoverDelay));
    }

    private IEnumerator AttackCo()
    {
        myRigidbody2D.velocity = Vector2.zero;
        currentState = PlayerState.attack;
        myAnimator.SetBool("isAttacking", true);
        yield return null;
        myAnimator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(attackDelay);
        currentState = PlayerState.idle;
    }

    private IEnumerator KnockCo(float knocktime, float recoverDelay)
    {
        if (myRigidbody2D != null && currentState != PlayerState.stagger)
        {
            currentState = PlayerState.stagger;
            yield return new WaitForSeconds(knocktime);
            myRigidbody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(recoverDelay);
            currentState = PlayerState.idle;
        }
    }
}

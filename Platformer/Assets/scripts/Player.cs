using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Use these settings to adjust player run speed.")]
    [SerializeField] private float runSpeed = 5.0f;
    [Tooltip("Use these settings to adjust player jump speed.")]
    [SerializeField] private float jumpSpeed = 5.0f;
    [Tooltip("Use these settings to adjust player climb speed.")]
    [SerializeField] private float climbSpeed = 5.0f;

    [Header("Death Settings")]
    [SerializeField] private Vector2 deathSeq = new Vector2(25f, 25f);

    private bool isAlive = true;

    float gravityScaleAtStart;

    Rigidbody2D playerCharacter;

    Animator playerAnimator;

    CapsuleCollider2D playerBodyCollider;

    BoxCollider2D playerFeetCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtStart = playerCharacter.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        Jump();
        Climb();
        Die();
    }

    private void Run()
    {
        // Value between -1 to +1
        float hMovement = Input.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2(hMovement * runSpeed, playerCharacter.velocity.y);
        playerCharacter.velocity = runVelocity;

        // playerAnimator.SetBool("run", true);

        // print(runVelocity);

        bool hSpeed = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;

        playerAnimator.SetBool("run", hSpeed);
    }

    private void FlipSprite()
    {
        // If the player is moving horizontally
        bool hMovement = Mathf.Abs(playerCharacter.velocity.x) > Mathf.Epsilon;

        if (hMovement)
        {
            // Reverse the current direction (scale) of the X-Axis
            transform.localScale = new Vector2(Mathf.Sign(playerCharacter.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // Will stop this function unless true
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            // Get new Y velocity based on a variable
            Vector2 jumpVelocity = new Vector2(0.0f, jumpSpeed);
            playerCharacter.velocity += jumpVelocity;
            // playerAnimator.SetBool("jump", true);
        }
        /* else
        {
            playerAnimator.SetBool("jump", false);
        } */
    }

    private void Climb()
    {
        if (!playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerAnimator.SetBool("climb", false);

            playerCharacter.gravityScale = gravityScaleAtStart;

            // Will stop this function unless true
            return;
        }

        // "Vertical" from Input Axes
        float vMovement = Input.GetAxis("Vertical");

        // X axis needs to remain the same and we need to change Y
        Vector2 climbVelocity = new Vector2(playerCharacter.velocity.x, vMovement * climbSpeed);
        playerCharacter.velocity = climbVelocity;

        playerCharacter.gravityScale = 0.0f;

        bool vSpeed = Mathf.Abs(playerCharacter.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("climb", vSpeed);
    }

    private void Die()
    {
        if(playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("die"); // Not yet created
            playerCharacter.velocity = deathSeq;

            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }
}

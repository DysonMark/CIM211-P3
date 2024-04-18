using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D characterController2D; //Referencing Brackey's 2D controller

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    [SerializeField] public float runSpeed = 40f;       // Run speed multiplier
    [SerializeField] public Animator playerAnimator;    // Reference to the player animator

    [SerializeField] public Transform groundCheck;      // Reference to a game object which stores the coordinates of the lowest point of the player's sprite
    [SerializeField] public float groundCheckRadius;    // Radius of the gizmo which will check if the player is standing on an object
    [SerializeField] public LayerMask whatIsGround;     // Layers of objects in which the player can stand on
    [SerializeField] public float jumpForce = 1000f;
    private bool isTouchingGround;                      // Parameter to check if the player is standing on an object
    private Rigidbody2D player;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //      Horizontal movement
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        playerAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));


        //      Jumping code
        //isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);  // Checks if the player is touching the ground

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
        }


        //      Crouching code
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouch = true;
        } 
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouch = false;
        }
    }
    //                      [JUMPING ANIMATION]

    // When the player enters a trigger with an object with the layer "Ground", sets the isTouchingGround bool variable to TRUE.
    void OnTriggerEnter2D(Collider2D ground)
    {
        if (ground.gameObject.layer == LayerMask.NameToLayer("GroundTilemap"))
        {
            isTouchingGround = true;
            playerAnimator.SetBool("isJumping", false);
            Debug.Log("Is touching ground");
        }
    }
    // When the player exits the trigger with an object with the layer "Ground", sets the isTouchingGround bool variable to FALSE.
    private void OnTriggerExit2D(Collider2D ground)
    {
        if (ground.gameObject.layer == LayerMask.NameToLayer("GroundTilemap"))
        {
            isTouchingGround = false;
            playerAnimator.SetBool("isJumping", true);
            Debug.Log("Is touching ground");
        }
    }

    private void FixedUpdate()
    {
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, false); 


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC1 : MonoBehaviour
{
    


    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
    and a counter to track how long you have been jumping*/

    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    public float movementSpeed;

  /*this bool is to tell us whether you are on the ground or not
   * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
   * ground objects to this layer.
   * The stoppedJumping bool lets us track when the player stops jumping.*/

    public bool grounded;
    public LayerMask whatIsGround;
    public bool stoppedJumping;

  /*the public transform is how you will detect whether we are touching the ground.
   * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
   * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    

    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :slight_smile:
    private Rigidbody2D rb;

    void Start()
    {
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GroundedCheck();
    }

    void FixedUpdate()
    {
        Jump();
        SideMovement();


    }

    private void Jump()
    {
        //I placed this code in FixedUpdate because we are using phyics to move.

        //if you press down the mouse button...
        if (Input.GetButton("Jump"))
        {
            //and you are on the ground...
            if (grounded)
            {
                //jump!
                //rb.velocity = new Vector2(rb.velocity.x, (jumpForce));
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                stoppedJumping = false;
            }
        }

        //if you keep holding down the jump button...
        if ((Input.GetButton("Jump")) && !stoppedJumping)
        {
            //and your counter hasn't reached zero...
            if (jumpTimeCounter > 0)
            {
                //keep jumping!
                //rb.velocity = new Vector2(rb.velocity.x, (jumpForce*jumpTimeCounter));
                rb.AddForce(new Vector2(0, jumpForce*jumpTimeCounter));
                
                jumpTimeCounter -= Time.deltaTime;
            }
        }


        //if you stop holding down the jump button...
        if (!Input.GetButton("Jump"))
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
         
            
            
            stoppedJumping = true;
        }
    }

    private void GroundedCheck()
    {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //if we are grounded...
        if (grounded)
        {
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }
    }


    private void SideMovement()
    {
        float sideMovement = Input.GetAxis("Horizontal");
        if(grounded == true)
        {
            transform.position += new Vector3(sideMovement * movementSpeed, 0f);
        }
        else
        {
            transform.position += new Vector3(sideMovement * movementSpeed/2, 0f);
        }
        
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }


}

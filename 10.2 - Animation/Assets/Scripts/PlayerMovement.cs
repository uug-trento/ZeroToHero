using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // These variables are private, they can be accessed only through this script

    private Rigidbody rb;           // The player's RigidBody component
    private Vector3 force;          // The overall force we use to move the player
    private float x, y, z;          // Force components       
    private float originalSpeed;    // Used to reset the speed after running   
    private bool isGrounded;        // Used to check if the player is touching the ground
    private int jumpsCounter;       // Count the number of jumps    

    private Vector3 offset;

    // These variables are public, they can be set in the editor --> inspector

    public float walkSpeed, 
                runSpeed, 
                jumpSpeed;
    public GameObject camera;
    public int totalJumps;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        originalSpeed = walkSpeed;          // Set the speed reset variable
        offset = camera.transform.position - transform.position;
    }

    private void Update()
    {
        // At each frame, let the camera follow the player with an offset
        camera.transform.position = transform.position + offset;
    }

    // Check collisions
    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with an object tagged "Terrain"
        if(collision.gameObject.tag == "Terrain")
        {  
            isGrounded = true;              // Set player to grounded
            jumpsCounter = totalJumps;      // Reset jumps
        }
    }

    void FixedUpdate()
    {
        // Get movement input (keyboard: arrows or WASD)
        x = Input.GetAxis("Horizontal");    
        z = Input.GetAxis("Vertical");

        if(!isGrounded)
        {
            x = x / 2;
            z /= 2;
        }

        // If the player is on the ground or with one jump left
        // and the player presses the spacebar ONCE
        if ( (isGrounded || jumpsCounter > 0) && Input.GetKeyDown(KeyCode.Space))
        {
            y = 1.0f;                           // Set vertical versor
            isGrounded = false;                 // The player is no more on the ground
            jumpsCounter = jumpsCounter - 1;    // Decrement the jumps counter
        }
        else
        {
            y = 0;                              // Else, reset vertical versor
        }

        // While "Shift" is being pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            walkSpeed = runSpeed;               // Set speed to run
        }
        else
        {
            walkSpeed = originalSpeed;          // Reset speed if not running
        }

        // Build the force in the three directions and apply it to the player's rigidbody
        force = new Vector3(0, y * jumpSpeed, 0);

        rb.velocity = new Vector3(x * walkSpeed, rb.velocity.y, z * walkSpeed);
        rb.AddForce(force);
    }

    public void AddDoubleJump()
    {
        totalJumps = 2;
    }
}

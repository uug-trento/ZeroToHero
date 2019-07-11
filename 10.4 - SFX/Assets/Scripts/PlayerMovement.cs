using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // These variables are private, they can be accessed only through this script

    private Rigidbody rb;           // The player's RigidBody component
    private Vector3 force;          // The overall force we use to move the player
    private float x, y, z;          // Force components       
    private float originalSpeed;    // Used to reset the speed after running   
    private bool isGrounded,        // Used to check if the player is touching the ground
                canJump;
    private int jumpsCounter;       // Count the number of jumps
    private AudioSource audioSource; 

    //private Vector3 offset;

    // These variables are public, they can be set in the editor --> inspector

    public GameObject cam;
    public float walkSpeed, 
                runSpeed, 
                jumpSpeed;
    //public GameObject camera;
    public int totalJumps;

    public Image powerupSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        originalSpeed = walkSpeed;          // Set the speed reset variable
        //offset = camera.transform.position - transform.position;
        canJump = true;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // At each frame, let the camera follow the player with an offset
        //camera.transform.position = transform.position + offset;
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


        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0;
        cameraForward = cameraForward.normalized;
        Vector3 cameraRight = cam.transform.right;
        cameraRight.y = 0;
        cameraRight = cameraRight.normalized;

        Vector3 movementInput = cameraForward * z + cameraRight * x;

        if (movementInput.sqrMagnitude > 1) {
            movementInput.Normalize();
        }


        // If the player is on the ground or with one jump left
        // and the player presses the spacebar ONCE
        if ( (isGrounded || jumpsCounter > 0) && canJump && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("JumpDelay");

            movementInput.y = 1.0f;                           // Set vertical versor
            isGrounded = false;                 // The player is no more on the ground
            jumpsCounter = jumpsCounter - 1;    // Decrement the jumps counter
            audioSource.Play();
        }
        else
        {
            movementInput.y = 0;                              // Else, reset vertical versor
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
        force = new Vector3(0, movementInput.y * jumpSpeed, 0);

        rb.velocity = new Vector3(movementInput.x * walkSpeed, rb.velocity.y, movementInput.z * walkSpeed);
        rb.AddForce(force);
    }

    public void AddDoubleJump()
    {
        jumpsCounter++;
        totalJumps = 2;
        powerupSprite.color = Color.white;
    }

    IEnumerator JumpDelay(){
        canJump = false;
        yield return new WaitForSeconds(0.1f);
        canJump = true;
    }
}

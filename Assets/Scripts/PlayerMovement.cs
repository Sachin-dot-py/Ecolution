using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb; // Rigidbody of Sprite
    public Animator animator;
    public int RunSpeed = 10; // Speed of horizontal movement 
    public int JumpSpeed = 10; // Speed of jumping
    public float JumpTime = 2.0f; // Maximum jump time
    public float Smoothening = .05f; // For smoothening player movement
    public bool MoveAllowed = true;
    private bool FacingRight; // Is the player facing right?
    private float JumpTimeCounter; // Counting time elapsed jumping
    private bool grounded; // If player is touching ground
    private Vector2 zero = Vector2.zero; // For Vector2.SmoothDamp in horizontal movement


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpTimeCounter = JumpTime;
        FacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x)); // Setting run speed in animator to change animation
        animator.SetBool("Grounded", grounded);
    }

    // Called at a fixed interval independent of frame rate. Add physics code here
    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal"); // Horizontal (left or right) movement
        float Vertical = Input.GetAxis("Vertical"); // Vertical (up or down) movement
        
        if (Vertical < 0){
            // Prevent downwards movement
            Vertical = 0;
        }

        if (Vertical > 0 && grounded){
            // Reset counter if grounded
            JumpTimeCounter = JumpTime;
        }

        if (JumpTimeCounter <= 0){
            // Exceeded jump limit
            Vertical = 0;
        }

        if (!grounded){
            // Update counter if jumping
            JumpTimeCounter -= Time.deltaTime;
        }

        if (Horizontal < 0 & FacingRight || Horizontal > 0 & !FacingRight){
            transform.Rotate(0f, 180f, 0f);
            FacingRight = !FacingRight;
        }
        
        if (MoveAllowed){
            // Running:
            Vector2 newVelocity = new Vector2(Horizontal * RunSpeed, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, newVelocity, ref zero, Smoothening);

            // Jumping:
            transform.Translate(Vector2.up * Time.deltaTime * Vertical * JumpSpeed);
        }

        // Vector2 jumpVelocity = new Vector2(0f, Vertical * JumpSpeed);
        // rb.AddForce(jumpVelocity);

        // Vector2 newVelocity = new Vector2(Horizontal * RunSpeed, Vertical * JumpSpeed);
        // rb.velocity = Vector2.SmoothDamp(rb.velocity, newVelocity, ref zero, smoothing);

        // Vector2 movement = new Vector2(Horizontal * RunSpeed, Vertical * JumpSpeed);
        // rb.AddForce(movement);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            grounded = true;
            JumpTimeCounter = JumpTime;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ground"){
            grounded = false;
            JumpTimeCounter = JumpTime;
        }
        if (other.gameObject.tag == "FakeGround"){
            gameObject.SendMessage("PowerUp","You got TRICKED");
            grounded = false;
            JumpTimeCounter = JumpTime;
        }
        }
}

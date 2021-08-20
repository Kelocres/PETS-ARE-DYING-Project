using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 3;
    private float moveInput;
    //private Rigidbody2D rb;
    private Rigidbody rb;
    private bool facingRight = true;

    private Animator animator;
    public bool ableToWalk = true;

    //Geting the input from ButtonsMovement
    private float inputTouch;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inputTouch = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal"); //comment this line out, and use the methods below for other movement methods

        moveInput += inputTouch;
        if(moveInput < -1f)     moveInput = -1f;
        else if(moveInput > 1f) moveInput = 1f;
        

        //NO MOVEMENT ALLOWED WHILE THE DIALOG SYSTEM IS ACTIVATED
        if(!ableToWalk) moveInput = 0f;

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == false && moveInput > 0) {
            Flip();
        } else if (facingRight == true && moveInput < 0) {
            Flip();
        }

        //For animate the sprite
        if(moveInput==0)    animator.SetBool("isWalking",false);
        else                animator.SetBool("isWalking",true);
    }

    void Flip() {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void goRight() {
        moveInput = 1;
    }
    public void goLeft() {
        moveInput = -1;
    }
    public void stop() {
        moveInput = 0;
    }

    public void InputButtonsMovement(string dir, bool state)
    {
        if(state)
        {
            if(dir=="left")         inputTouch = -1f;
            else if(dir=="right")   inputTouch = 1f; 
        }
        
        else inputTouch = 0f;
    }
    
}

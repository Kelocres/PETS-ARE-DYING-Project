using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 3;
    private float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        moveInput = Input.GetAxis("Horizontal"); //comment this line out, and use the methods below for other movement methods

        Debug.Log(moveInput);
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
}

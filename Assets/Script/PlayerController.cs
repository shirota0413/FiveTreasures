using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private ContactFilter2D filter2d;
    [SerializeField]
    private LayerMask groundMask;

    public int hp;

    //bool isJump;
    float axis;
    bool isGround;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    void Start() {
        animator =  GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    void PlayerMove() {
        Vector2 velocity = rb.velocity;
        axis = Input.GetAxis("Horizontal");

        //isGround = rb.IsTouching(filter2d);
        GroundCheck();
        if (Input.GetButtonDown("Jump") && isGround) {
            velocity.y = 5;
            
            //isJump = true;
        }
        if (axis != 0) {
            spriteRenderer.flipX = axis < 0;
            velocity.x = axis * speed;
        }
        rb.velocity = velocity;
    }

    void Animation() { 
        animator.SetBool("IsJump", !isGround);
        animator.SetFloat("VelocityY", rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(axis));
    }

    void GroundCheck() {
        isGround = Physics2D.Raycast(this.transform.position,Vector3.down, 1.0f, groundMask);
    }

    void FixedUpdate() {
        //PlayerMove();
    }
    void Update() {
        PlayerMove();
        
        Animation();
    }
}

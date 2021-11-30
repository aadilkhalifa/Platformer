using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb;
    public Animator animator;
    bool isFacingLeft;
    Vector2 startPosition;
    bool isGrounded = false;
    public Transform groundCheck;
    public LayerMask groundLayer; 

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        isFacingLeft = false;
        // transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        startPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
        if(Input.GetKey(KeyCode.Space) && isGrounded == true){
            rb.velocity = new Vector2(rb.velocity.x, speed);
            animator.SetBool("isJumping", true);
        } else if(rb.velocity.y < 0.01){
            animator.SetBool("isJumping", false);
        }

        if(isFacingLeft==false && rb.velocity.x<0){
            isFacingLeft = true; 
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        } else if(isFacingLeft==true && rb.velocity.x>0){
            isFacingLeft = false; 
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        } 

        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));

        if(transform.position.y < -8)
        {
            transform.position = startPosition;
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void onLanding(){
        animator.SetBool("isJumping", false);
    }
}

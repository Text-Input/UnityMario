using UnityEngine;
using System.Collections;

public class MarioController : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    public float moveSpeed = 15f;
    public float sliding = 5f;
    public float jumpPower = 50f;
    bool isGrounded = true;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {

        float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        float xv = rb.velocity.x;
        float yv = rb.velocity.y;

        // if moving
        if (h != 0) {
            rb.velocity = new Vector2(h * moveSpeed, yv);
            transform.localScale = new Vector2(Mathf.Sign(h), transform.localScale.y);
        }
        // if sliding
        else {
            rb.velocity = new Vector2(xv * sliding, yv);
        }

        // setting the "Speed" float in the animator
        anim.SetFloat("Speed", Mathf.Abs(h));
        

        if (Input.GetKey(KeyCode.Space)) {
            // do a jump
            if (isGrounded) {
                Vector2 jump = new Vector2(rb.velocity.x, jumpPower);
                rb.velocity = jump;
                isGrounded = false;
            }
        }

        anim.SetBool("Jumping", !isGrounded);
    }


    void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.tag == "Solid") isGrounded = true;
    }
}

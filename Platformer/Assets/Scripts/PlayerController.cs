using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jump;
    public int score = 0;

    public Transform topLeft;
    public Transform bottomRight;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public LayerMask ground;

    bool isDead;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isDead = false;
    }


    void FixedUpdate() {

        if (isDead) return;

        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h != 0) {
            if (h < 0) {
                sr.flipX = true;
            } else {
                sr.flipX = false;
            }
        }

        rb.velocity = new Vector2(h * speed * Time.deltaTime, rb.velocity.y);

        bool isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, ground);

        anim.SetBool("Jumping", !isGrounded);

        if (isGrounded && Input.GetKey(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("Enemy")) {

            BoxCollider2D enemyCollider = other.gameObject.GetComponent<BoxCollider2D>();

            float enemyTop = other.gameObject.transform.position.y + (enemyCollider.size.y / 2);

            if (transform.position.y > enemyTop) {
                other.gameObject.GetComponent<EnemyController>().Die();

                rb.velocity = new Vector2(rb.velocity.x, jump);
            } else {
                StartCoroutine(playerDeath());
            }

        }
        if (other.gameObject.CompareTag("Coin")) {
            other.gameObject.SetActive(false);
            score++;

        }
        if (other.gameObject.CompareTag("EndFlag")){
            //Application.LoadLevel(Application.loadedLevel + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coin")) {
            other.gameObject.SetActive(false);
            score++;
        }
        
    }

    IEnumerator playerDeath() {

        isDead = true;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);
        

        rb.velocity = new Vector2(0, 15);
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(restartLevel());

    }
    IEnumerator restartLevel() {

        yield return new WaitForSeconds(1.9f);
        Application.LoadLevel(Application.loadedLevel);


    }
}

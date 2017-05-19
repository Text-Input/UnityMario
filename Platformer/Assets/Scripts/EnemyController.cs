using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    bool isDead;
	// Use this for initialization
	void Start () {
        isDead = false;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
	}
	
	
	void FixedUpdate () {
        if (isDead) return;

        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (speed > 0) {
            sr.flipX = false;
        }else if (speed < 0) {
            sr.flipX = true;
        }

	}
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Barrier")) {
            speed *= -1;
        }
    }
    public void Die() {
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        isDead = true;
        StartCoroutine(Delete());
    }

    IEnumerator Delete() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

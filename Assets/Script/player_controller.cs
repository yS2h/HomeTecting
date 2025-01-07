using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private Animator am;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.A)) {
            am.SetInteger("input", 2);
            rb.AddForce(transform.right * -1 * moveSpeed, ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.D)) {
            am.SetInteger("input", 1);
            rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        }
        else 
            am.SetInteger("input", 0);
    }
}
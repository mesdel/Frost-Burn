using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedForce;
    public float jumpForce;
    private Rigidbody2D playerRB;
    public bool isGrounded;

    private float fallGrace = -2.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    void Update()
    {
        if (isGrounded && playerRB.velocity.y < fallGrace)
            isGrounded = false;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerRB.AddForce(Vector2.right * speedForce * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerRB.AddForce(Vector2.left * speedForce * Time.deltaTime);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.UpArrow))
            && isGrounded)
        {
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    // todo: prevent wall jumps / head collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // todo: prevent mid-air jumps after player falls off a ledge
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    } */
}

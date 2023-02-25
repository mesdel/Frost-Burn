using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedForce;
    public float jumpForce;
    public float maxSpeed;

    private Rigidbody2D playerRB;
    public bool isGrounded;

    private float fallGrace = -2.5f;
    private float frictionLand = 0.1f;
    private float friction = 0.3f;
    
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
            Jump();
        }

        Vector2 currVelocity = playerRB.velocity;
        playerRB.velocity = new Vector2(Mathf.Clamp(currVelocity.x, -maxSpeed, maxSpeed), currVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.CompareTag("Ground")
            && collision.otherCollider.gameObject.CompareTag("Feet"))
        {
            Land();

            // the following may cause issues?
            gameObject.transform.SetParent(collision.collider.gameObject.transform, true);
        }
    }
    
    private void Land()
    {
        isGrounded = true;
        playerRB.sharedMaterial.friction = friction;
    }

    private void Jump()
    {
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
        playerRB.sharedMaterial.friction = frictionLand;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    } 
}

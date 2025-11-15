using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2;
    public float jumpDelay = 0.1f;
    private Rigidbody2D rb;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (Keyboard.current.wKey.isPressed && isGrounded())
        {
            rb.linearVelocity = new Vector2(0, jumpHeight);
        }
        if (Keyboard.current.wKey.wasReleasedThisFrame && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y * jumpDelay);
        }
    }
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
    */

    public bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer)){
            return true;
        }

        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
      
        Gizmos.DrawWireCube(transform.position-transform.up* castDistance, boxSize);
    }
}
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runAcceleration = 50;
    public float slowdownAccel = 20;
    public float jumpHeight = 2;
    public float jumpDelay = 0.1f;
    public float jumpGravity = 10;
    public float jumpEndGravity = 60;
    public float fallGravity = 20;
    public float maxFallSpeed = 30;
    private Rigidbody2D rb;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public bool isLeft = false;
    public float pagesCollected = 0;
    


    public Vector2 velocity;
    private float gravity;
    [SerializeField]
    private bool isGrounded;
    private bool isJumping;

    private Vector2 moveVel;

    private bool active = true;
    public void Enable()
    {
        active = true;
    }
    public void Disable()
    {
        active = false;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = fallGravity;
    }

    void Update()
    {
        //velocity = rb.linearVelocity;
        moveVel.x = velocity.x;
        if (!active)
        {
            isJumping = false;
            return;
        }

        if (!Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            isGrounded = false;
        }
        if (isGrounded && !isJumping)
        {
            velocity.y = 0;
        }

        Vector2 moveDir = Vector2.zero;
        if (Mathf.Abs(MoveInput().x) > 0.1)
        {
            moveDir = new Vector2(MoveInput().x, 0).normalized * speed;
        }

        float accel = runAcceleration;
        if (Mathf.Abs(velocity.x) > speed && !isGrounded)
            accel = slowdownAccel;

        moveVel = Vector2.MoveTowards(moveVel, moveDir, accel * Time.deltaTime);
        velocity.x = moveVel.x;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Jump();
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && !isFalling())
        {
            //velocity.y = rb.linearVelocity.y * jumpDelay;
            gravity = jumpEndGravity;
            isJumping = false;
        }
        if (isFalling() && !isGrounded)
        {
            isJumping = false;
        }
        if ((isFalling() || isGrounded) && !isJumping)
        {
            gravity = fallGravity;
        }

        velocity.y -= gravity * Time.deltaTime;
        if (isFalling())
        {
            velocity.y = Mathf.Clamp(velocity.y, -maxFallSpeed, 0);
        }


        rb.linearVelocity = velocity;
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (var coll in collision.contacts)
        {
            if (Vector2.Angle(coll.normal, Vector2.up) < 30)
            {
                isGrounded = false;
            }
        }
    }*/


    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var coll in collision.contacts)
        {
            if (Vector2.Angle(coll.normal, Vector2.up) < 30)
            {
                isGrounded = true;
                velocity.y = 0;
            }
        }

        if (collision.gameObject.CompareTag("Page"))
        {
            collision.gameObject.SetActive(false);
            pagesCollected++;

        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var coll in collision.contacts)
        {
            //if (coll.normal)
        }
    }

    public void Jump()
    {
        velocity.y = jumpHeight;
        gravity = jumpGravity;
        isJumping = true;
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public bool isFalling()
    {
        return velocity.y < 0 && !isGrounded;
    }

    public void SetVelocity(Vector2 newVelocity)
    {
        velocity = newVelocity;
        rb.linearVelocity = newVelocity;
    }

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public Vector2 MoveInput()
    {
        Vector2 move = Vector2.zero;
        if (Keyboard.current.aKey.isPressed)
        {
            move.x -= 1;
            isLeft = true;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            move.x += 1;
            isLeft = false;
        }
        if (Keyboard.current.wKey.isPressed)
        {
            move.y += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            move.y -= 1;
        }
        return move;
    }
}
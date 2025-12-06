using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwing : MonoBehaviour
{
    public bool isSwinging;
    public float SlideSpeed = 3;
    public float jumpSpeed = 10;

    private PlayerMovement movement;

    private Vine vine;
    private float vineDist;

    private bool active = false;
    public void Enable()
    {
        active = true;
    }
    public void Disable()
    {
        active = false;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }

        if (isSwinging)
        {
            if (movement.MoveInput().y < 0)
            {
                vineDist += SlideSpeed * Time.deltaTime;
            }
            if (movement.MoveInput().y > 0)
            {
                vineDist -= SlideSpeed * Time.deltaTime;
            }

            vineDist = Mathf.Clamp(vineDist, 1, vine.vineLength - 1);

            transform.localPosition = -Vector2.up * vineDist;
            transform.localEulerAngles = Vector3.zero;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                isSwinging = false;

                if (movement != null)
                {
                    float speed;
                    if (Mathf.Abs(movement.MoveInput().x) > 0.1)
                    {
                        if (movement.MoveInput().x > 0)
                        {
                            speed = jumpSpeed;
                        }
                        else
                        {
                            speed = -jumpSpeed;
                        }
                    }
                    else
                    {
                        if (transform.up.x < 0)
                        {
                            speed = jumpSpeed;
                        }
                        else
                        {
                            speed = -jumpSpeed;
                        }
                    }
                    if (movement.MoveInput().y < 0)
                    {
                        speed = 0;
                    }
                    else
                    {
                        movement.Jump();
                    }
                    movement.SetVelocity(new Vector2(speed, movement.velocity.y));
                }

                transform.parent = null;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSwinging || movement == null) 
        {
            return;
        }

        vine = collision.gameObject.GetComponent<Vine>();
        if (vine != null && !movement.IsGrounded())
        {
            isSwinging = true;
            transform.SetParent(vine.transform);
            movement.SetVelocity(Vector2.zero);

            vineDist = (transform.position - vine.transform.position).magnitude;
            transform.localPosition = -Vector2.up * vineDist;
            transform.localEulerAngles = Vector3.zero;
        }
    }
}

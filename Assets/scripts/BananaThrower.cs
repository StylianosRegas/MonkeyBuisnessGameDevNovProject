using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class BananaThrower : MonoBehaviour
{
    public GameObject Banana;
    public int launchForceX = 5;
    public int launchForceY = 5;
    public int angularForce = 300;
    public float cooldownDuration = 1.0f;

    public PlayerMovement playerMovement;

    private float cooldownTimer = 0;
    private Vector2 spawnPosition;
    private Vector2 spawnVelocity;

    private bool active = true;
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
        spawnVelocity = new Vector2(2,2);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && cooldownTimer <= 0 )
        {

            if(playerMovement.isLeft)
            {
                spawnPosition = new Vector2(transform.position.x - 2, transform.position.y + 2);
                GameObject newBanana = Instantiate(Banana, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = newBanana.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(-launchForceX, launchForceY);
                rb.angularVelocity = new Vector2(0, 300).magnitude;
            }
            else
            {
                spawnPosition = new Vector2(transform.position.x + 2, transform.position.y + 2);
                GameObject newBanana = Instantiate(Banana, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = newBanana.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(launchForceX, launchForceY);
                rb.angularVelocity = new Vector2(0, 300).magnitude;
            }
            
            
            cooldownTimer = cooldownDuration;

        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

}

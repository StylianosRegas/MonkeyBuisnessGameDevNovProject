using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class BananaThrower : MonoBehaviour
{
    public GameObject Banana;
    public float bananaSize = 1;
    public Vector2 spawnOffset; 
    public float minThrowSpeed = 10;
    public float maxThrowSpeed = 10;
    public float minThrowAngle = 30;
    public float maxThrowAngle = 30;
    //public float launchForceX = 5;
    //public float launchForceY = 5;
    public float angularForce = 300;
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

            float angVel = Random.Range(-angularForce, angularForce);
            float throwAng = Random.Range(minThrowAngle, maxThrowAngle);
            float throwSpeed = Random.Range(minThrowSpeed, maxThrowSpeed);

            if(playerMovement.isLeft)
            {
                spawnPosition = new Vector2(transform.position.x - spawnOffset.x, transform.position.y + spawnOffset.y);
                GameObject newBanana = Instantiate(Banana, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = newBanana.GetComponent<Rigidbody2D>();
                newBanana.transform.localScale = Vector3.one * bananaSize;
                //rb.linearVelocity = new Vector2(-launchForceX, launchForceY) + playerMovement.velocity;
                Vector2 throwDir = Quaternion.AngleAxis(-throwAng, Vector3.forward) * -Vector2.right * throwSpeed;
                rb.linearVelocity = throwDir + playerMovement.velocity;
                rb.angularVelocity = angVel;
            }
            else
            {
                spawnPosition = new Vector2(transform.position.x + spawnOffset.x, transform.position.y + spawnOffset.y);
                GameObject newBanana = Instantiate(Banana, spawnPosition, Quaternion.identity);
                Rigidbody2D rb = newBanana.GetComponent<Rigidbody2D>();
                newBanana.transform.localScale = Vector3.one * bananaSize;
                //rb.linearVelocity = new Vector2(launchForceX, launchForceY) + playerMovement.velocity;
                Vector2 throwDir = Quaternion.AngleAxis(throwAng, Vector3.forward) * Vector2.right * throwSpeed;
                rb.linearVelocity = throwDir + playerMovement.velocity;
                rb.angularVelocity = angVel;
            }
            
            
            cooldownTimer = cooldownDuration;

        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

}

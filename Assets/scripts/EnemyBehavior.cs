using UnityEngine;
using UnityEngine.Rendering;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 velocity;
    private Transform currPosition;



    public GameObject pointA;
    public GameObject pointB;
    public float speed = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       
       currPosition = pointA.transform;
    }

    // Update is called once per frame
    void Update()
    {



        Vector2 point = currPosition.position - transform.position;

        if (currPosition == pointA.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
            
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);

        }

        if(Vector2.Distance(transform.position, currPosition.position) < 0.5f && currPosition == pointA.transform)
        {
           currPosition = pointB.transform;
        }

        if (Vector2.Distance(transform.position, currPosition.position) < 0.5f && currPosition == pointB.transform)
        {
            currPosition = pointA.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("BananaPro"))
        {
            Destroy(gameObject);
        }
        
    }

    
}

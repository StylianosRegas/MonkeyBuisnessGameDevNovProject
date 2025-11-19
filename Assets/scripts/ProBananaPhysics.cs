using UnityEngine;

public class ProBananaPhysics : MonoBehaviour
{
    public float respawnTime = .5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject, respawnTime);
        
    }
}

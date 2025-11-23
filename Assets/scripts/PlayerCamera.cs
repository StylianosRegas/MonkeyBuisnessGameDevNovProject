using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public float lerpSpeed = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector3 targetPos = new Vector3(player.position.x, player.position.y, -10);

        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * lerpSpeed); ;
    }
}

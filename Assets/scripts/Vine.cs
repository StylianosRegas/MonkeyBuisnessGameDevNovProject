using UnityEngine;

public class Vine : MonoBehaviour
{
    public float vineLength = 10;
    public float swingAngle = 30;
    public float swingSpeed = 1;

    private float timer = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        float vineAngle = Mathf.Sin(timer * swingSpeed) * swingAngle;
        transform.eulerAngles = new Vector3 (0, 0, vineAngle);
    }
}

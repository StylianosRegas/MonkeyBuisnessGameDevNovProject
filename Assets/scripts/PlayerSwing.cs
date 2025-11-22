using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    public bool isSwinging;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        }


    }
}

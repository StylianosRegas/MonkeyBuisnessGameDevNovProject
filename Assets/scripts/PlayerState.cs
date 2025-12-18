using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] protected PlayerMovement playerMove;
    [SerializeField] private BananaThrower bananaThrow;
    [SerializeField] private PlayerSwing swing;

    [SerializeField] private MoveState moveState;

    public float pagesCollected;
    public bool finalDoor = false;

    private enum MoveState
    {
        Moving,
        Swinging
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        pagesCollected = playerMove.pagesCollected;
        if (swing.isSwinging && moveState != MoveState.Swinging)
        {
            moveState = MoveState.Swinging;
            playerMove.Disable();
            bananaThrow.Disable();
            swing.Enable();
        }
        if (!swing.isSwinging && moveState == MoveState.Swinging)
        {
            moveState = MoveState.Moving;
            playerMove.Enable();
            bananaThrow.Enable();
            swing.Disable();
        }

        if(pagesCollected == 4)
        {
            finalDoor = true;   
        }
    }
}

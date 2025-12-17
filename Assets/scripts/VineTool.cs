using UnityEngine;

[ExecuteInEditMode]
public class VineTool : MonoBehaviour
{
    private Vine vine;
    private BoxCollider2D hitbox;
    private LineRenderer line;

    [SerializeField] private Transform endSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vine == null)
        {
            vine = GetComponent<Vine>();
            if (vine == null)
            {
                print("VineTool requires VineScript");
            }
        }
        if (vine != null)
        {
            if (hitbox == null)
            {
                hitbox = vine.GetComponent<BoxCollider2D>();
                if (hitbox == null)
                {
                    print("Vine does not contain BoxCollider2D");
                }
            }
            if (line == null)
            {
                line = vine.GetComponent<LineRenderer>();
                if (line == null)
                {
                    print("Vine does not have LineRenderer");
                }
            }
        }

        if (vine != null && hitbox != null)
        {
            hitbox.size = new Vector2(hitbox.size.x, vine.vineLength);
            hitbox.offset = new Vector2(0, -vine.vineLength * 0.5f);

            if (line != null)
            {
                line.SetPositions(new Vector3[] { vine.transform.position, vine.transform.position - (vine.transform.up * vine.vineLength) });
            }
        }

        if (endSprite != null)
        {
            endSprite.transform.position = vine.transform.position - (vine.transform.up * vine.vineLength);
        }
    }
}

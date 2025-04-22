using UnityEngine;

public class DiverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer SpriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // Flip sprite BARA när spelaren trycker
        if (moveX > 0)
        {
            SpriteRenderer.flipX = true;  // Tittar höger (flippad)
        }
        else if (moveX < 0)
        {
            SpriteRenderer.flipX = false; // Tittar vänster (original)
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}


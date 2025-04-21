using UnityEditor.Callbacks;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer SpriteRenderer; // Reference to the SpriteRenderer componentS

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized; // Normalize to prevent faster diagonal movement

        if (moveX != 0)
        {
            SpriteRenderer.flipX = moveX < 0; // Flip the sprite based on movement direction
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed; // Apply movement to the Rigidbody2D
    }
}

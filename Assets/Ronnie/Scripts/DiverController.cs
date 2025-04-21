using UnityEditor.Callbacks;
using UnityEngine;

public class DiverController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer SpriteRenderer; // Reference to the SpriteRenderer componentS

    private bool isFacingRight = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.flipX = false;
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveX > 0 && !isFacingRight)
        {
            SpriteRenderer.flipX = true;
            isFacingRight = true;
        }
        else if (moveX < 0 && isFacingRight)
        {
            SpriteRenderer.flipX = false;
            isFacingRight = false;
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed; // Apply movement to the Rigidbody2D
    }
}

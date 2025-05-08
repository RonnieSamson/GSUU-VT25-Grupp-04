using UnityEngine;

public class DiverLandController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite;   // när spelaren står still
    [SerializeField] private Sprite walkSprite;   // när spelaren rör sig

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
    }

    void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
    }

    void OnDisable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("Grounded: " + isGrounded);

        // Rörelse
        rb.linearVelocity = new Vector2(moveX * walkSpeed, rb.linearVelocity.y);

        // Vänd sprite
        if (moveX > 0)
            spriteRenderer.flipX = true;
        else if (moveX < 0)
            spriteRenderer.flipX = false;

        // Sprite: stå still eller gå
        if (Mathf.Abs(moveX) > 0.01f)
            spriteRenderer.sprite = walkSprite;
        else
            spriteRenderer.sprite = idleSprite;

        // Hop
    }
}
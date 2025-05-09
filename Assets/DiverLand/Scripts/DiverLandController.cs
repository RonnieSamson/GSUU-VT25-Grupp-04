using UnityEngine;

public class DiverLandController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite;   
    [SerializeField] private Sprite walkSprite;  

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
 


        rb.linearVelocity = new Vector2(moveX * walkSpeed, rb.linearVelocity.y); 


        if (moveX > 0)
            spriteRenderer.flipX = true;
        else if (moveX < 0)
            spriteRenderer.flipX = false;


        if (Mathf.Abs(moveX) > 0.01f)
            spriteRenderer.sprite = walkSprite;
        else
            spriteRenderer.sprite = idleSprite;


        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
    }
}

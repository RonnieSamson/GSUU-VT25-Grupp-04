using UnityEngine;

public class DiverLandController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 3f;

    [Header("Sprites")]
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite walkingSprite;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveDirection;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
    }

    void Update()
    {
        if (isDead) return;

        float moveX = Input.GetAxisRaw("Horizontal");

        moveDirection = new Vector2(moveX, 0f).normalized;

        if (moveX != 0f)
        {
            spriteRenderer.sprite = walkingSprite;
            spriteRenderer.flipX = moveX > 0;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * walkSpeed;
    }

    
}

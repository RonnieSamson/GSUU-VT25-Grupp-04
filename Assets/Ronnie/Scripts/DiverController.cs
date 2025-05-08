using UnityEngine;

public class DiverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [Header("Sprites")]
    [SerializeField] private Sprite neutralSprite;
    [SerializeField] private Sprite swimmingSprite;

    [Header("Boost Sprites")]
    [SerializeField] private Sprite boostNeutralSprite;
    [SerializeField] private Sprite boostSwimmingSprite;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private Transform respawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer SpriteRenderer;

    public bool isDead = false;
    private bool isRespawning = false;

    // Boost
    private bool isBoosted = false;
    private float boostTimeRemaining = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = neutralSprite;
    }

    void Update()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Hantera boost-timer
        if (isBoosted)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostTimeRemaining <= 0f)
            {
                isBoosted = false;
            }
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0f || moveY != 0f)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;

            // Välj rätt sprite
            SpriteRenderer.sprite = isBoosted ? boostSwimmingSprite : swimmingSprite;

            // Flip beroende på riktning
            if (moveX > 0)
                SpriteRenderer.flipX = true;
            else if (moveX < 0)
                SpriteRenderer.flipX = false;
        }
        else
        {
            moveDirection = Vector2.zero;

            // Välj neutral sprite
            SpriteRenderer.sprite = isBoosted ? boostNeutralSprite : neutralSprite;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        moveDirection = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        isBoosted = false;

        if (!isRespawning)
            Invoke(nameof(Respawn), respawnDelay);
        isRespawning = true;
    }

    public void ActivateBoost(float duration)
    {
        isBoosted = true;
        boostTimeRemaining = duration;
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;

        isDead = false;
        isRespawning = false;
        SpriteRenderer.sprite = neutralSprite;

        // Återställ luft
        AirTimer air = FindObjectOfType<AirTimer>();
        if (air != null)
            air.ResetAir();
    }
}

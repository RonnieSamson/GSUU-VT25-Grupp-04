
using UnityEngine;

public class DiverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

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
    private SpriteRenderer spriteRenderer;

    public bool isDead = false;
    private bool isRespawning = false;
    private bool isBoosted = false;
    private float boostTimeRemaining = 0f;
    private float originalMoveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = neutralSprite;
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (isBoosted)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostTimeRemaining <= 0f)
            {
                isBoosted = false;
                moveSpeed = originalMoveSpeed;
                spriteRenderer.sprite = neutralSprite;
            }
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0f || moveY != 0f)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
            spriteRenderer.sprite = isBoosted ? boostSwimmingSprite : swimmingSprite;

            if (moveX > 0) spriteRenderer.flipX = true;
            else if (moveX < 0) spriteRenderer.flipX = false;
        }
        else
        {
            moveDirection = Vector2.zero;
            spriteRenderer.sprite = isBoosted ? boostNeutralSprite : neutralSprite;
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
        {
            Invoke(nameof(Respawn), respawnDelay);
            isRespawning = true;
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        isDead = false;
        isRespawning = false;

        spriteRenderer.sprite = neutralSprite;

        AirTimer air = FindFirstObjectByType<AirTimer>();
        if (air != null)
        {
            air.ResetAir();
        }

        DeathManager deathManager = FindFirstObjectByType<DeathManager>();
        if (deathManager != null)
        {
            deathManager.ResetDeath();
        }
    }

    // Används av flaskan (AirBoostBottle)
    public void ActivateBoost(float duration)
    {
        isBoosted = true;
        boostTimeRemaining = duration;
    }

    // Används av shoppen (fenor)
    public void ActivateFinsBoost(float boostSpeed, float duration)
    {
        isBoosted = true;
        boostTimeRemaining = duration;
        moveSpeed = boostSpeed;
    }
}



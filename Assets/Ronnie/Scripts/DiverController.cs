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
    private SpriteRenderer spriteRenderer;

    public bool isDead = false;
    private bool isRespawning = false;
    private bool isBoosted = false;
    private float boostTimeRemaining = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = neutralSprite;
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
            boostTimeRemaining -= Time.unscaledDeltaTime;
            if (boostTimeRemaining <= 0f)
                isBoosted = false;
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
            StartCoroutine(RespawnAfterDelay());
            isRespawning = true;
        }
    }

    private System.Collections.IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSecondsRealtime(respawnDelay);
        Respawn();
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
        spriteRenderer.sprite = neutralSprite;

        AirTimer air = FindAnyObjectByType<AirTimer>();
        if (air != null)
        {
            air.ResetAir();
        }

        DeathManager deathManager = FindAnyObjectByType<DeathManager>();
        if (deathManager != null)
        {
            deathManager.ResetDeath();
        }
    }
}

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

    [Header("Fins Sprites")]
    [SerializeField] private Sprite finsNeutralSprite;
    [SerializeField] private Sprite finsSwimmingSprite;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private Transform respawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;
    private bool isRespawning = false;

    // Boost logic
    private bool isBoosted = false;
    private float boostTimeRemaining = 0f;

    // Fins logic
    private bool finsActive = false;
    private float finsTimer = 0f;
    private float finsDuration = 15f;
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

        // Boost countdown
        if (isBoosted)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostTimeRemaining <= 0f)
                isBoosted = false;
        }

        // Fins countdown
        if (finsActive)
        {
            finsTimer -= Time.deltaTime;
            if (finsTimer <= 0f)
                DeactivateFins();
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0f || moveY != 0f)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;

            if (isBoosted)
                spriteRenderer.sprite = boostSwimmingSprite;
            else if (finsActive)
                spriteRenderer.sprite = finsSwimmingSprite;
            else
                spriteRenderer.sprite = swimmingSprite;

            spriteRenderer.flipX = moveX > 0 ? true : moveX < 0 ? false : spriteRenderer.flipX;
        }
        else
        {
            moveDirection = Vector2.zero;

            if (isBoosted)
                spriteRenderer.sprite = boostNeutralSprite;
            else if (finsActive)
                spriteRenderer.sprite = finsNeutralSprite;
            else
                spriteRenderer.sprite = neutralSprite;
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

    public void ActivateBoost(float duration)
    {
        isBoosted = true;
        boostTimeRemaining = duration;
    }

    public void ActivateFins()
    {
        finsActive = true;
        finsTimer = finsDuration;
        moveSpeed = originalMoveSpeed * 2f; // justera om du vill ha annan boost
    }

    private void DeactivateFins()
    {
        finsActive = false;
        moveSpeed = originalMoveSpeed;
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
        isDead = false;
        isRespawning = false;

        spriteRenderer.sprite = neutralSprite;

        AirTimer air = FindAnyObjectByType<AirTimer>();
        if (air != null)
            air.ResetAir();

        DeathManager deathManager = FindAnyObjectByType<DeathManager>();
        if (deathManager != null)
            deathManager.ResetDeath();
    }
}

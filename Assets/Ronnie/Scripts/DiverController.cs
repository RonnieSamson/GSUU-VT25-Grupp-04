using UnityEngine;

public class DiverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;

    [Header("Sprites")]
    [SerializeField] private Sprite neutralSprite;
    [SerializeField] private Sprite swimmingSprite;

    [Header("Boost Sprites")]
    [SerializeField] private Sprite bottleBoostNeutralSprite;
    [SerializeField] private Sprite bottleBoostSwimmingSprite;
    [SerializeField] private Sprite finsBoostNeutralSprite;
    [SerializeField] private Sprite finsBoostSwimmingSprite;

    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 3f;
    [SerializeField] private Transform respawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

    public bool isDead = false;
    private bool isRespawning = false;

    private float boostTimeRemaining = 0f;
    private float finsBoostTimeRemaining = 0f;

    private bool bottleBoostActive = false;
    private bool finsBoostActive = false;

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

        // Hantera bottle boost
        if (bottleBoostActive)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostTimeRemaining <= 0f)
            {
                bottleBoostActive = false;
            }
        }

        // Hantera fins boost
        if (finsBoostActive)
        {
            finsBoostTimeRemaining -= Time.deltaTime;
            if (finsBoostTimeRemaining <= 0f)
            {
                finsBoostActive = false;
                moveSpeed = originalMoveSpeed;
            }
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0f || moveY != 0f)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;

            if (finsBoostActive)
                spriteRenderer.sprite = finsBoostSwimmingSprite;
            else if (bottleBoostActive)
                spriteRenderer.sprite = bottleBoostSwimmingSprite;
            else
                spriteRenderer.sprite = swimmingSprite;

            spriteRenderer.flipX = moveX > 0f;
        }
        else
        {
            moveDirection = Vector2.zero;

            if (finsBoostActive)
                spriteRenderer.sprite = finsBoostNeutralSprite;
            else if (bottleBoostActive)
                spriteRenderer.sprite = bottleBoostNeutralSprite;
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

        bottleBoostActive = false;
        finsBoostActive = false;
        moveSpeed = originalMoveSpeed;

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

    // Flask-boost (för AirBoostBottle)
    public void ActivateBottleBoost(float duration)
    {
        bottleBoostActive = true;
        boostTimeRemaining = duration;
    }

    // Legacy-kompatibilitet för gamla flask-script
    public void ActivateBoost(float duration)
    {
        ActivateBottleBoost(duration);
    }

    // Fenor från shop
    public void ActivateFinsBoost(float boostSpeed, float duration)
    {
        finsBoostActive = true;
        finsBoostTimeRemaining = duration;
        moveSpeed = boostSpeed;
    }
}

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
    [SerializeField] private Sprite soloTubeNeutralSprite;
    [SerializeField] private Sprite soloTubeSwimmingSprite;

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
    private float airTubeTimeRemaining = 0f;

    private bool bottleBoostActive = false;
    private bool finsBoostActive = false;
    private bool airTubeActive = false;

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

        if (bottleBoostActive)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostTimeRemaining <= 0f)
                bottleBoostActive = false;
        }

        if (finsBoostActive)
        {
            finsBoostTimeRemaining -= Time.deltaTime;
            if (finsBoostTimeRemaining <= 0f)
            {
                finsBoostActive = false;
                moveSpeed = originalMoveSpeed;
            }
        }

        if (airTubeActive)
        {
            airTubeTimeRemaining -= Time.deltaTime;
            if (airTubeTimeRemaining <= 0f)
                airTubeActive = false;
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
            else if (airTubeActive)
                spriteRenderer.sprite = soloTubeSwimmingSprite;
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
            else if (airTubeActive)
                spriteRenderer.sprite = soloTubeNeutralSprite;
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
        airTubeActive = false;
        moveSpeed = originalMoveSpeed;

        if (!isRespawning)
        {
            StartCoroutine(RespawnCoroutine());
            isRespawning = true;
        }
    }

    private System.Collections.IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSecondsRealtime(respawnDelay);
        Respawn();
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

    public void ActivateBoost(float duration)
    {
        bottleBoostActive = true;
        boostTimeRemaining = duration;
    }

    public void ActivateFinsBoost(float boostSpeed, float duration)
    {
        finsBoostActive = true;
        finsBoostTimeRemaining = duration;
        moveSpeed = boostSpeed;
    }

    public void ActivateAirTube(float duration)
    {
        airTubeActive = true;
        airTubeTimeRemaining = duration;
    }
}

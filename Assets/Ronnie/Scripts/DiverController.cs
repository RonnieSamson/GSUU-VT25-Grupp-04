using UnityEngine;
using System.Collections;

public class DiverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    [Header("Sprites")]
    [SerializeField] private Sprite neutralSprite;
    [SerializeField] private Sprite swimmingSprite;

    [Header("Boost Sprites")]
    [SerializeField] private Sprite boostNeutralSprite;
    [SerializeField] private Sprite boostSwimmingSprite;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer SpriteRenderer;

    public bool isDead = false;

    private Coroutine gravityCoroutine;
    private bool isTransitioningGravity = false; 

    // Boost
    private bool isBoosted = false;
    private float boostTimeRemaining = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = neutralSprite;
    }

    void OnEnable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();


        if (gravityCoroutine != null) StopCoroutine(gravityCoroutine);
        gravityCoroutine = StartCoroutine(ChangeGravitySmoothly(0f));
    }

    void OnDisable()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();


        if (gravityCoroutine != null) StopCoroutine(gravityCoroutine);
        gravityCoroutine = StartCoroutine(ChangeGravitySmoothly(1f));
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
            SpriteRenderer.sprite = isBoosted ? boostSwimmingSprite : swimmingSprite;

            if (moveX > 0)
                SpriteRenderer.flipX = true;
            else if (moveX < 0)
                SpriteRenderer.flipX = false;
        }
        else
        {
            moveDirection = Vector2.zero;
            SpriteRenderer.sprite = isBoosted ? boostNeutralSprite : neutralSprite;
        }
    }

    void FixedUpdate()
    {
        if (isTransitioningGravity) return; 

        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void Die()
    {
        isDead = true;
        moveDirection = Vector2.zero;
    }

    public void ActivateBoost(float duration)
    {
        isBoosted = true;
        boostTimeRemaining = duration;
    }


    private IEnumerator ChangeGravitySmoothly(float targetGravity)
    {
        isTransitioningGravity = true;

        float duration = 0.5f;
        float startGravity = rb.gravityScale;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            rb.gravityScale = Mathf.Lerp(startGravity, targetGravity, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        rb.gravityScale = targetGravity;
        isTransitioningGravity = false;
    }
}

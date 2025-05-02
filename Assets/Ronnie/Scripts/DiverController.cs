using UnityEngine;

public class DiverController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Sprite neutralSprite;
    [SerializeField] private Sprite swimmingSprite;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private SpriteRenderer SpriteRenderer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();


        SpriteRenderer.sprite = neutralSprite; // Sätter den neutrala sprite som standard
    }

    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0f || moveY != 0f)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;
            SpriteRenderer.sprite = swimmingSprite;  // Simmande sprite visas

            // Flip baserat på horisontell riktning
            if (moveX > 0)
            {
                SpriteRenderer.flipX = true;  // Tittar höger
            }
            else if (moveX < 0)
            {
                SpriteRenderer.flipX = false; // Tittar vänster
            }
        }
        else
        {
            // Om INGEN input – byt tillbaka till neutral sprite
            moveDirection = Vector2.zero;
            SpriteRenderer.sprite = neutralSprite;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
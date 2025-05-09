using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;
    public float fleeSpeed = 6f;
    public float fleeDuration = 2f;
    public float detectionRadius = 3f;
    public LayerMask threatLayer;

    private Vector3 startPos;
    private bool movingRight = true;
    private SpriteRenderer[] spriteRenderers;

    private bool isFleeing = false;
    private float fleeTimer = 0f;

    void Start()
    {
        startPos = transform.position;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in spriteRenderers)
            sr.flipX = !movingRight;
    }

    void Update()
    {
        // Kolla om något hot är nära
        Collider2D threat = Physics2D.OverlapCircle(transform.position, detectionRadius, threatLayer);
        if (threat && !isFleeing)
        {
            isFleeing = true;
            fleeTimer = fleeDuration;
            movingRight = transform.position.x < threat.transform.position.x; // fly motsatt riktning
            foreach (var sr in spriteRenderers)
                sr.flipX = !movingRight;
        }

        if (isFleeing)
        {
            fleeTimer -= Time.deltaTime;
            if (fleeTimer <= 0f)
                isFleeing = false;
        }

        float currentSpeed = isFleeing ? fleeSpeed : speed;
        float moveStep = currentSpeed * Time.deltaTime;
        transform.position += (movingRight ? Vector3.right : Vector3.left) * moveStep;

        // Byt håll vid max distans (bara om inte flyr)
        if (!isFleeing)
        {
            float movedDistance = Vector3.Distance(transform.position, startPos);
            if (movedDistance >= distance)
            {
                movingRight = !movingRight;
                startPos = transform.position;
                foreach (var sr in spriteRenderers)
                    sr.flipX = !movingRight;
            }
        }
    }
}

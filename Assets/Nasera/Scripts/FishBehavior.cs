using UnityEngine;

public class FishBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;

    private Vector3 startPos;
    private bool movingRight = true;
    private SpriteRenderer[] spriteRenderers;

    void Start()
    {
        startPos = transform.position;
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Sätt rätt startvändning
        foreach (var sr in spriteRenderers)
            sr.flipX = !movingRight;
    }

    void Update()
    {
        // Rörelse
        float moveStep = speed * Time.deltaTime;
        if (movingRight)
            transform.position += Vector3.right * moveStep;
        else
            transform.position += Vector3.left * moveStep;

        // Vänd vid max distans
        float movedDistance = Vector3.Distance(transform.position, startPos);
        if (movedDistance >= distance)
        {
            movingRight = !movingRight; // Byt håll
            startPos = transform.position; // Starta ny vända från nuvarande position

            // Vänd alla fiskar visuellt
            foreach (var sr in spriteRenderers)
                sr.flipX = !movingRight;
        }
    }
}

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

        // S�tt r�tt startv�ndning
        foreach (var sr in spriteRenderers)
            sr.flipX = !movingRight;
    }

    void Update()
    {
        // R�relse
        float moveStep = speed * Time.deltaTime;
        if (movingRight)
            transform.position += Vector3.right * moveStep;
        else
            transform.position += Vector3.left * moveStep;

        // V�nd vid max distans
        float movedDistance = Vector3.Distance(transform.position, startPos);
        if (movedDistance >= distance)
        {
            movingRight = !movingRight; // Byt h�ll
            startPos = transform.position; // Starta ny v�nda fr�n nuvarande position

            // V�nd alla fiskar visuellt
            foreach (var sr in spriteRenderers)
                sr.flipX = !movingRight;
        }
    }
}

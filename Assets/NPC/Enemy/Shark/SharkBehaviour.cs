using UnityEngine;

public class SharkBehaviour : MonoBehaviour
{
    public GameObject patrol1; //Patrol point 1
    public GameObject patrol2; //Patrol point 2
    //public Transform player; When player is implemented, uncomment and reference in Start() as well

    private Rigidbody2D rb;
    private Transform currentPoint;

    public float speed = 7.0f;
    public float stoppingDistance = 0.2f; // How close we need to get to a patrol point

    private float life = 10f; // hp of the shark
    private float damage = 1f; //damage per bite
    public float attackRange = 5.0f; // Range to start chasing the player
    private bool isChasingPlayer = false;

    // Attributes for turning "animation"
    private bool facingRight = true;
    private bool isTurning = false;
    private float turnSpeed = 225f; // degrees per second
    private Quaternion targetRotation;
    private Vector3 originalScale;
    private Vector3 biggerScale;
    private float scaleLerpSpeed = 4f; // how fast it shrinks and grows
    private bool shrinking = true;
    private float originalSpeed;
    private float slowedSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = patrol1.transform;
        originalScale = transform.localScale;
        biggerScale = originalScale * 1.15f; // 15% bigger during turns
        originalSpeed = speed;
        slowedSpeed = speed * 0.3f; // 30% of the original speed during turns
    }

    void Update()
    {
        //float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        //// Check if we should attack player
        //if (distanceToPlayer < attackRange)
        //{
        //    isChasingPlayer = true;
        //}
        //else
        //{
        //    isChasingPlayer = false;
        //}

        // Smoothly adjust speed back to normal after turning
        
    }

    void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (isChasingPlayer) // Needs the player gameobject to work, not tested at all just a placholder
        {
            //Vector2 direction = (player.position - transform.position).normalized;
            //rb.linearVelocity = direction * speed;
        }

        else
        {
            // Patrol between points
            Vector2 direction = (currentPoint.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            // Check if close enough to the patrol point
            if (Vector2.Distance(transform.position, currentPoint.position) < stoppingDistance)
            {
                // Switch to the other patrol point
                if (currentPoint == patrol1.transform)
                {
                    currentPoint = patrol2.transform;
                }
                else
                {
                    currentPoint = patrol1.transform;
                }

                // Flip the shark
                Flip();
            }

        }

        if (isTurning)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // scaling up and down during turn to simulate 3d movement
            if (shrinking)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, biggerScale, scaleLerpSpeed * Time.deltaTime);

                // If small enough, start growing back
                if (Vector3.Distance(transform.localScale, biggerScale) < 0.01f)
                {
                    shrinking = false;
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleLerpSpeed * Time.deltaTime);

                // If back to normal size and finished rotating, stop the turn
                if (Vector3.Distance(transform.localScale, originalScale) < 0.01f && Quaternion.Angle(transform.rotation, targetRotation) < 1f)
                {
                    transform.localScale = originalScale;
                    transform.rotation = targetRotation;
                    isTurning = false;
                }
            }
        }

        if (!isTurning && speed != originalSpeed)
        {
            speed = Mathf.Lerp(speed, originalSpeed, 1.2f * Time.deltaTime);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        // Set the target rotation
        float yRotation = facingRight ? 180f : 0f;
        targetRotation = Quaternion.Euler(0f, yRotation, 0f);

        // Start turning, shrinking and slowing
        isTurning = true;
        shrinking = true;
        speed = slowedSpeed;
    }
}

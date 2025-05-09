using UnityEngine;
using UnityEngine.Audio;

public class SharkBehaviour : MonoBehaviour
{
    public GameObject patrol1; //Patrol point 1
    public GameObject patrol2; //Patrol point 2
    public Transform player;
    public AirTimer airTimer;
    private Rigidbody2D rb;
    private Transform currentPoint;


    [Header("Attributes for movement and life")]
    private float speed = 4f; // slightly slower than the player
    private float stoppingDistance = 0.2f; // How close we need to get to a patrol point before turning 
    private float life = 10f; // hp of the shark


    [Header("Attributes for chasing & attacking")]
    private float damage = 1f; //damage per bite
    private float attackRange = 1.0f; // Range to start attacking the player
    private float chaseRange = 20f; // Range to start chasing the player
    private bool isChasingPlayer = false;

    private float biteCooldown = 3f;// time before shark can bite again
    private float chaseResetTime = 2f;// time after bite before returning to patrol
    private float lastBiteTime = -999f;
    private bool coolingDown = false;

    public ParticleSystem bubbles;
    public AudioSource bubbleSfx;

    public AudioSource ScreamOuchSfx;

    [Header("Attributes for turning \"animation\"")] 
    private bool facingRight = true;
    private bool isTurning = false;
    private float turnSpeed = 225f; //in degrees per second
    private Quaternion targetRotation;
    private Vector3 originalScale;
    private Vector3 biggerScale;
    private float scaleLerpSpeed = 4f; // how fast it shrinks and grows via linear interpolation
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
        DistanceToPlayerCheck();

        if (isChasingPlayer)
        {
            float directionToPlayer = player.position.x - transform.position.x;

            // Kontrollera om hajen behöver vändas
            if ((directionToPlayer > 0 && !facingRight) || (directionToPlayer < 0 && facingRight))
            {
                Flip();
            }
        }
    }


    void FixedUpdate()
    {
        Patrol();
    }

    private void Attack()
{
    if (Time.time - lastBiteTime >= biteCooldown)
    {
        var health = player.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(1);
        }

        bubbles.Play();
        bubbleSfx.Play();
        ScreamOuchSfx.Play();
        lastBiteTime = Time.time;

        isChasingPlayer = false;
        coolingDown = true;
    }
}


    private void DistanceToPlayerCheck()
    {
        if (!coolingDown)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            // Check if we should attack player
            if (distanceToPlayer < attackRange)
            {
                Attack();
            }

            else if (distanceToPlayer < chaseRange)
            {
                isChasingPlayer = true;
            }

            else
            {
                isChasingPlayer = false;
            }
        }

        if (coolingDown && Time.time - lastBiteTime > chaseResetTime)
        {
            isChasingPlayer = false;
            coolingDown = false;
        }
    }


    private void Patrol()
    {
        if (isChasingPlayer)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            // Räkna ut riktning till patrullpunkt
            Vector2 direction = (currentPoint.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            // 🔁 Kolla om hajen behöver vändas mot patrullpunkten
            float directionToPoint = currentPoint.position.x - transform.position.x;
            if ((directionToPoint > 0 && !facingRight) || (directionToPoint < 0 && facingRight))
            {
                Flip();
            }

            // Byt patrullpunkt om vi är nära
            if (Vector2.Distance(transform.position, currentPoint.position) < stoppingDistance)
            {
                currentPoint = currentPoint == patrol1.transform ? patrol2.transform : patrol1.transform;
            }
        }

        // Hantering för 3D-vändningseffekt
        if (isTurning)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            if (shrinking)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, biggerScale, scaleLerpSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.localScale, biggerScale) < 0.01f)
                {
                    shrinking = false;
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleLerpSpeed * Time.deltaTime);

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

using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [Header("Död och Game Over")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private SpriteRenderer diverRenderer;
    [SerializeField] private GameObject gameOverText;

    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private DiverController diver;
    private bool hasDied = false;

    void Start()
    {
        diver = FindAnyObjectByType<DiverController>();
        audioSource = GetComponent<AudioSource>();
        gameOverText.SetActive(false);
    }

    public void TriggerDeath()
    {
        if (hasDied) return;
        hasDied = true;

        diverRenderer.sprite = deadSprite;
        gameOverText.SetActive(true);

        if (diver != null)
        {
            diver.Die();
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.LogWarning("AudioSource eller deathSound saknas i DeathManager.");
        }

        Time.timeScale = 0f; // Pausar spelet – respawn körs ändå tack vare coroutine
    }

    public void ResetDeath()
    {
        hasDied = false;
        gameOverText.SetActive(false);
        Time.timeScale = 1f;
    }
}

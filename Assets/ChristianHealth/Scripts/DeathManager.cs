using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DeathManager : MonoBehaviour   
{
    [Header("Död och Game Over")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private SpriteRenderer diverRenderer;
    [SerializeField] private GameObject gameOverText;

    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private bool hasDied = false;
    private DiverController diver;

    void Start()
    {
        gameOverText.SetActive(false); 
        diver = FindObjectOfType<DiverController>();
        audioSource = GetComponent<AudioSource>();
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
            Debug.LogWarning("AudioSource or deathSound is not assigned in DeathManager.");
        }
        Time.timeScale= 0;
    }
}

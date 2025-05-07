using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DeathManager : MonoBehaviour   
{
    [Header("Död och Game Over")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private SpriteRenderer diverRenderer;
    [SerializeField] private GameObject gameOverText;

    private bool hasDied = false;
    private DiverController diver;

    void Start()
    {
        gameOverText.SetActive(false); 
        diver = FindObjectOfType<DiverController>();
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
        Time.timeScale= 0;
    }
}

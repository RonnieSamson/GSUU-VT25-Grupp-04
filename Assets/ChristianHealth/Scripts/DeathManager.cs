using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DeathManager : MonoBehaviour   
{
    [Header("Död och Game Over")]
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private SpriteRenderer diverRenderer;
    [SerializeField] private GameObject gameOverText;

    private bool hasDied = false;

    void Start()
    {
        gameOverText.SetActive(false); 
    }

    public void TriggerDeath()
    {
        if (hasDied) return;
        hasDied = true;

        diverRenderer.sprite = deadSprite;
        gameOverText.SetActive(true);

        Time.timeScale= 0;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public Image[] hearts; 
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private DiverController diver;

    private DeathManager deathManager;

    void Start()
    {
        currentHealth = maxHealth;
        diver = GetComponent<DiverController>();
        deathManager = FindAnyObjectByType<DeathManager>();
        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
             currentHealth = 0;
             if (deathManager != null)
             {
                 deathManager.TriggerDeath(); 
            }
        }
         UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}

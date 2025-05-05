using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirTimer : MonoBehaviour
{
  [SerializeField] private float startTime = 10f;
  public float currentTime;

  [SerializeField] private Text airTimerText;
  [SerializeField] private Image airTimerBar;

  [SerializeField] private Color normalColor = Color.green;
  [SerializeField] private Color warningColor = Color.red;

  [Header("Död och Game Over")]
  [SerializeField] private Sprite deadSprite;
  [SerializeField] private SpriteRenderer diverRenderer;
  [SerializeField] private GameObject gameOverText;

  private bool hasDied = false;

  void Start()
  {
    currentTime = startTime;
    gameOverText.SetActive(false);
  }

  void Update()
  {
    if (hasDied) return;

    if (currentTime > 0f)
    {
      currentTime -= Time.deltaTime;
      if (currentTime < 0f) currentTime = 0f;

      int minutes = Mathf.FloorToInt(currentTime / 60f);
      int seconds = Mathf.FloorToInt(currentTime % 60f);
      airTimerText.text = $"{minutes:00}:{seconds:00}";

      float fillAmount = currentTime / startTime;
      airTimerBar.fillAmount = fillAmount;

      if (currentTime <= 5f)
      {
        airTimerText.color = warningColor;
        airTimerBar.color = warningColor;
      }
      else
      {
        airTimerText.color = normalColor;
        airTimerBar.color = normalColor;
      }
    }
    else
    {
      hasDied = true;
      diverRenderer.sprite = deadSprite;
      gameOverText.SetActive(true);

      // Meddela DiverController att dykaren är död
      DiverController diver = FindObjectOfType<DiverController>();
      if (diver != null)
      {
        diver.Die();
      }
    }
  }
  public void AddAir(float extraTime)
  {
    currentTime = Mathf.Min(currentTime + extraTime, startTime);
  }
}

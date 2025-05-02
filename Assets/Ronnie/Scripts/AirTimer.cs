using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements; // OBS: EJ nödvändig – TA BORT om du inte använder UI Toolkit!
using TMPro; // Endast om du använder TextMeshPro, annars ta bort

public class AirTimer : MonoBehaviour
{
  [SerializeField] private float startTime = 90f; // 1 min 30 sek
  private float currentTime;

  [SerializeField] private Text airTimerText; // Din UI Text-komponent
  [SerializeField] private Image airTimerBar; // Din gröna mätare (Image med Fill)
  [SerializeField] private Color normalColor = Color.green;
  [SerializeField] private Color warningColor = Color.red;

  private void Start()
  {
    currentTime = startTime;
  }

  private void Update()
  {
    if (currentTime > 0f)
    {
      currentTime -= Time.deltaTime;
      if (currentTime < 0f) currentTime = 0f;
    }

    // Räkna ut minuter och sekunder
    int minutes = Mathf.FloorToInt(currentTime / 60f);
    int seconds = Mathf.FloorToInt(currentTime % 60f);
    airTimerText.text = $"{minutes:0}:{seconds:00}";

    // Fyll mätaren
    float fillAmount = currentTime / startTime;
    airTimerBar.fillAmount = fillAmount;

    // Ändra färg
    if (currentTime <= 15f)
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
}

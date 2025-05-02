using UnityEngine;
using UnityEngine.UI;

public class AirTimer : MonoBehaviour
{
  [SerializeField] private float startTime = 90f; // 1 minut och 30 sekunder
  private float currentTime;

  [SerializeField] private Text airTimerText;

  void Start()
  {
    currentTime = startTime;
  }

  void Update()
  {
    if (currentTime > 0f)
    {
      currentTime -= Time.deltaTime;
      if (currentTime < 0f) currentTime = 0f;
    }

    // Formatera till minuter och sekunder
    int minutes = Mathf.FloorToInt(currentTime / 60f);
    int seconds = Mathf.FloorToInt(currentTime % 60f);
    airTimerText.text = $"{minutes:0}:{seconds:00}";

    // Byt färg på texten de sista 15 sekunderna
    if (currentTime <= 15f)
    {
      airTimerText.color = Color.red;
    }
    else
    {
      airTimerText.color = Color.green;
    }
  }
}

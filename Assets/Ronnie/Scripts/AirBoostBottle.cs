using UnityEngine;

public class AirBoostBottle : MonoBehaviour
{
  public float airBoostAmount = 15f;
  public float boostDuration = 15f;
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      DiverController diver = other.GetComponent<DiverController>();
      AirTimer timer = Object.FindFirstObjectByType<AirTimer>();

      if (diver != null && timer != null)
      {
        // Add air boost to the diver
        timer.AddAir(airBoostAmount);
        diver.ActivateBoost(boostDuration);
      }
      Destroy(gameObject);
    }
  }
}



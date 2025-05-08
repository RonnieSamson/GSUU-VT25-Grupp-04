using UnityEngine;

public class AirBoostBottle : MonoBehaviour
{
  public float airBoostAmount = 15f;
  public float boostDuration = 15f;

  [SerializeField] private AudioClip airpressureSound;
    private AudioSource audioSource;

  private void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }
  
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
        // Play sound effect
        if (audioSource != null && airpressureSound != null)
        {
          audioSource.PlayOneShot(airpressureSound);
        }
        else
        {
          Debug.LogWarning("AudioSource or airpressureSound is not assigned in AirBoostBottle.");
        }
        
      }
      Destroy(gameObject);
    }
  }
}



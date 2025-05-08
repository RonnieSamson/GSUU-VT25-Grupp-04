using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aktivera simning
            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null)
            {
                diverSwim.enabled = true;
                diverLand.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Återgå till gång
            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null)
            {
                diverSwim.enabled = false;
                diverLand.enabled = true;
            }
        }
    }
}

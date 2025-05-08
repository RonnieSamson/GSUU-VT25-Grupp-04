using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    // När spelaren går in i vattnet
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hämta komponenter för DiverController och DiverLandController
            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null)
            {
                // Stäng av landkontrollen och aktivera simkontrollen
                diverLand.enabled = false;  // Stäng av gångläge
                diverSwim.enabled = true;   // Aktivera simning
                Debug.Log("Swim mode activated.");
            }
        }
    }

    // När spelaren lämnar vattnet
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hämta komponenter för DiverController och DiverLandController
            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null)
            {
                // Vi kontrollerar nu om spelaren är i vattnet när de lämnar triggern
                if (!diverSwim.enabled) // Om simläget inte är aktivt (spelaren inte är i vattnet)
                {
                    // Återgå till gångläge
                    diverSwim.enabled = false;  // Stäng av simkontrollen
                    diverLand.enabled = true;   // Aktivera gångläge
                    Debug.Log("Land mode activated.");
                }
            }
        }
    }
}

using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    // När spelaren går in i vattnet
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null)
            {
      
                diverLand.enabled = false;  
                diverSwim.enabled = true;  
                Debug.Log("Swim mode activated.");
            }
        }
    }

    // När spelaren lämnar vattnet
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null)
            {
   
                if (!diverSwim.enabled) 
                {
                    // Återgå till gångläge
                    diverSwim.enabled = false;  
                    diverLand.enabled = true;  
                    Debug.Log("Land mode activated.");
                }
            }
        }
    }
}

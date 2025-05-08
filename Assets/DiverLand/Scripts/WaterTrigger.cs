using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DiverController>().enabled = true;
            other.GetComponent<DiverLandController>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<DiverController>().enabled = false;
            other.GetComponent<DiverLandController>().enabled = true;
        }
    }
}

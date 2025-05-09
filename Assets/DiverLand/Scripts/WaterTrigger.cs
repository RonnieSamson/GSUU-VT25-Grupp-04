using UnityEngine;
using System.Collections;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null && diverLand.enabled)
            {
                diverLand.enabled = false;
                diverSwim.enabled = true;
                Debug.Log("Swim mode activated.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DiverController diverSwim = other.GetComponent<DiverController>();
            DiverLandController diverLand = other.GetComponent<DiverLandController>();

            if (diverSwim != null && diverLand != null && diverSwim.enabled)
            {
                diverSwim.StartCoroutine(SwitchToLandAfterGravity(diverSwim, diverLand));
            }
        }
    }

    private IEnumerator SwitchToLandAfterGravity(DiverController swim, DiverLandController land)
    {

        yield return new WaitForSeconds(0.5f); 

        swim.enabled = false;
        land.enabled = true;
        Debug.Log("Land mode activated (delayed).");
    }
}

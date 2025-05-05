using UnityEditor;
using UnityEngine;


public class WaterSpring : MonoBehaviour
{
    
    public float velocity = 200f;
    private float targetHeight;
    private float initialTargetHeight; // Spara det ursprungliga värdet för targetHeight

    [SerializeField]
    private float waveHeight = 10f; // Höjd på vågen

    private void Start()
    {
        // Spara det ursprungliga värdet för targetHeight
        initialTargetHeight = transform.localPosition.y + 1.0f;
        targetHeight = initialTargetHeight;
    }

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        // Beräkna avvikelsen från målhöjden och krafterna
        var x = transform.localPosition.y - targetHeight;
        var springForce = -springStiffness * x;
        var dampingForce = -dampening * velocity;

        // Uppdatera hastighet och position
        velocity = Mathf.Clamp(velocity + (springForce + dampingForce) * Time.fixedDeltaTime, -waveHeight, waveHeight);
        var y = transform.localPosition.y + velocity * Time.fixedDeltaTime;

        // Hantera ogiltiga värden
        if (float.IsNaN(y))
        {
            Debug.LogError("Y blev NaN! Återställer värden.");
            velocity = 0f;
            y = targetHeight;
        }

        // Uppdatera objektets position
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    
        public void ResetSpring()
        {
            // Återställ hastighet och målhöjd till det ursprungliga värdet
            velocity = waveHeight;
            targetHeight = initialTargetHeight; // Använd det sparade ursprungliga värdet

            // Återställ objektets position till targetHeight
            transform.localPosition = new Vector3(transform.localPosition.x, targetHeight, transform.localPosition.z);

            Debug.Log("Spring reset!");
        }

   
}
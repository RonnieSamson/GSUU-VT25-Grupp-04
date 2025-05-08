using UnityEngine;

public class PlantSway : MonoBehaviour
{
    public float swayAmount = 7.5f;         // Max rotation i grader
    public float swaySpeed = 0.5f;           // Svajhastighet

    private float startRotationZ;

    void Start()
    {
        startRotationZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.rotation = Quaternion.Euler(0, 0, startRotationZ + angle);
    }
}


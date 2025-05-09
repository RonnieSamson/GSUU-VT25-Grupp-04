using UnityEngine;

public class DistanceFade : MonoBehaviour
{
    public Transform targetCamera;
    public float startFadeDistance = 10f;
    public float endFadeDistance = 30f;
    public Color nearColor = Color.white;
    public Color farColor = Color.gray; // Mörkare färg
    public float nearAlpha = 1f;
    public float farAlpha = 0.5f; // Mer genomskinligt

    private Renderer objectRenderer;
    private Material objectMaterial;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            // Skapa en ny materialinstans för att inte påverka andra objekt som delar material
            objectMaterial = new Material(objectRenderer.material);
            objectRenderer.material = objectMaterial;
        }
        else
        {
            Debug.LogError("Objektet har ingen Renderer!");
            enabled = false;
        }

        if (targetCamera == null)
        {
            targetCamera = Camera.main.transform;
            if (targetCamera == null)
            {
                Debug.LogError("Ingen huvudkamera hittades. Ange en targetCamera.");
                enabled = false;
            }
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetCamera.position);

        if (distance >= startFadeDistance)
        {
            float t = Mathf.Clamp01((distance - startFadeDistance) / (endFadeDistance - startFadeDistance));
            Color currentColor = Color.Lerp(nearColor, farColor, t);
            float currentAlpha = Mathf.Lerp(nearAlpha, farAlpha, t);

            // Om materialet stöder _Color (standard)
            if (objectMaterial.HasProperty("_Color"))
            {
                currentColor.a = currentAlpha;
                objectMaterial.SetColor("_Color", currentColor);
            }
            // Om materialet använder ett annat färg-property-namn, justera här
            else if (objectMaterial.HasProperty("_BaseColor"))
            {
                Color baseColor = objectMaterial.GetColor("_BaseColor");
                baseColor = Color.Lerp(nearColor, farColor, t);
                baseColor.a = currentAlpha;
                objectMaterial.SetColor("_BaseColor", baseColor);
            }
        }
        else
        {
            // Återställ till nära färg och alpha om objektet är tillräckligt nära
            if (objectMaterial.HasProperty("_Color"))
            {
                Color resetColor = nearColor;
                resetColor.a = nearAlpha;
                objectMaterial.SetColor("_Color", resetColor);
            }
            else if (objectMaterial.HasProperty("_BaseColor"))
            {
                Color resetBaseColor = nearColor;
                resetBaseColor.a = nearAlpha;
                objectMaterial.SetColor("_BaseColor", resetBaseColor);
            }
        }
    }
}
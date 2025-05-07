using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public GameObject backgroundGroup;

    // Mjuka färger för dag och natt i havsmiljö
    public Color dayColor = new Color(0.4f, 0.8f, 1f);     // Ljus turkosblå
    public Color nightColor = new Color(0f, 0.1f, 0.3f);   // Mörkblå havsfärg

    public float cycleDuration = 30f; // Hela cykeln (dag till natt till dag)

    private SpriteRenderer[] renderers;
    private float timer;

    void Start()
    {
        if (backgroundGroup != null)
        {
            renderers = backgroundGroup.GetComponentsInChildren<SpriteRenderer>();
        }
    }

    void Update()
    {
        if (renderers == null || renderers.Length == 0) return;

        timer += Time.deltaTime;

        // PingPong ger ett mjukt fram-och-tillbaka-värde mellan 0 och 1
        float t = Mathf.PingPong(timer / cycleDuration, 1f);

        // Gör övergången ännu mjukare med SmoothStep
        t = t * t * (3f - 2f * t);

        Color currentColor = Color.Lerp(dayColor, nightColor, t);

        foreach (var renderer in renderers)
        {
            renderer.color = currentColor;
        }
    }
}






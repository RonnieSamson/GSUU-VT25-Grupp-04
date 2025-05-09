using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishGroups;
    public int minCount = 7;
    public int maxCount = 9;
    public Transform backgroundArea;

    void Start()
    {
        if (backgroundArea == null)
        {
            Debug.LogError("BackgroundArea is not assigned!");
            return;
        }

        // Använd bakgrundens storlek för att definiera området
        Bounds bounds = GetBounds(backgroundArea);

        foreach (GameObject fishGroup in fishGroups)
        {
            int amount = Random.Range(minCount, maxCount + 1);
            for (int i = 0; i < amount; i++)
            {
                Vector3 pos = new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    0
                );

                Instantiate(fishGroup, pos, Quaternion.identity, this.transform);
            }
        }
    }

    Bounds GetBounds(Transform area)
    {
        Bounds bounds = area.GetComponent<Collider2D>().bounds;
        // SpriteRenderer sr = area.GetComponent<SpriteRenderer>();
        if (bounds != null)
        {
            return bounds;
        }

        Debug.LogWarning("No SpriteRenderer found on backgroundArea, using default area.");
        return new Bounds(Vector3.zero, new Vector3(50, 30, 0)); // reservområde
    }
}
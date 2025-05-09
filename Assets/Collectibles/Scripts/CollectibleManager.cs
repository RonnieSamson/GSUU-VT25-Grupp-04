using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI collectibleText;

    private int totalCollectibles = 0;
    private int collected = 0;

    void Start()
    {
        // Räkna automatiskt antal collectibles i scenen
        totalCollectibles = FindObjectsOfType<Collectible>().Length;
        UpdateCollectibleText();
    }

    public void AddCollectible()
    {
        collected++;
        UpdateCollectibleText();
    }

    void UpdateCollectibleText()
    {
        collectibleText.text = $"{collected}/{totalCollectibles}";
    }
}

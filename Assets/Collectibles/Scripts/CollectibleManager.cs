using TMPro;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public TextMeshProUGUI collectibleText;

    private int totalCollectibles = 0;
    private int collected = 0;

    void Start()
    {
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

using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{ 
    public ShopManager shopManager;

    public void Start()
    {
        Debug.Log("ShopButton is alive!");
    }

    public void OnDestroy()
    {
        Debug.Log("ShopButton is dead!");
    }

    public void BuyAirtube()
    {
        shopManager.BuyUpgrade(5, "Airtube");
    }

    public void BuyFins()
    {
        shopManager.BuyUpgrade(5, "Fins");
    }

    public void BuyFillAir()
    {
        shopManager.BuyUpgrade(2, "FillAir");
    }
}

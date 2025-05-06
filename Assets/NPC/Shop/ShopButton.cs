using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{ 

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
        Debug.Log("Button Airtube");

        //shopManager.BuyUpgrade(5, "Airtube");
    }

    public void BuyFins()
    {
        Debug.Log("Button Fins");

        //shopManager.BuyUpgrade(5, "Fins");
    }

    public void BuyFillAir()
    {
        Debug.Log("Button Fillair");

        //shopManager.BuyUpgrade(2, "FillAir");
    }
}

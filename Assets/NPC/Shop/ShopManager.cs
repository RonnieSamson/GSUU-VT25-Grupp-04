using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject openShopText;
    public GameObject closeShopText;
    public GameObject btnAirTube;
    public GameObject btnFillAir;
    public GameObject btnFins;
    // public playerStats playerstats; n�got s�tt att komma �t spelarens attribut
    public float money;
    public ShopTrigger shopTrigger;
    public bool shopMenuIsOpen = false;

    public void Start()
    {
        openShopText.SetActive(false);
        closeShopText.SetActive(false);

        btnAirTube.SetActive(false);
        btnFillAir.SetActive(false);
        btnFins.SetActive(false);
    }

    void Update()
    {

        //if in range and menu not open
        if (shopTrigger.playerInRange && !shopMenuIsOpen)
        {
            openShopText.SetActive(true);
            closeShopText.SetActive(false);
        }

        //if menu open
        if (shopMenuIsOpen)
        {
            openShopText.SetActive(false);
            closeShopText.SetActive(true);
        }

        //if not in range
        if (!shopTrigger.playerInRange)
        {
            if (shopMenuIsOpen)
            {
                CloseShop();
            }
            closeShopText.SetActive(false);
            openShopText.SetActive(false);
        }


        if (shopTrigger.playerInRange && Input.GetKeyDown(KeyCode.E) && !shopMenuIsOpen)
        {
            Debug.Log("E pressed 1");

            OpenShop();
        }

        else if (shopMenuIsOpen && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed 2");

            CloseShop();
        }
    }

    public void OpenShop()
    {
       Time.timeScale = 0f; // Pause the game
        

        btnAirTube.SetActive(true);
        btnFillAir.SetActive(true);
        btnFins.SetActive(true);
        shopMenuIsOpen = true;
    }

    public void CloseShop()
    {
        Time.timeScale = 1f; // unpause the game
        btnAirTube.SetActive(false);
        btnFillAir.SetActive(false);
        btnFins.SetActive(false);
        shopMenuIsOpen = false;

    }

    public void BuyUpgrade(int cost, string upgradeType)
    {
        if (money >= cost)
        {
            money -= cost;

            switch (upgradeType) // L�gg till uppgradering h�r i. T.ex Player.swimSpeed = 15;
            {
                case "Airtube":
                    Debug.Log("Airtube bought");

                    break;

                case "Fins":
                    Debug.Log("Fins bought");
                    
                    break;

                case "FillAir":
                    Debug.Log("FillAir bought");

                    break;
            }
        }
    }
}

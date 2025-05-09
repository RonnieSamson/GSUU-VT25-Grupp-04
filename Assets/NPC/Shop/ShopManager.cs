using UnityEngine;


public class ShopManager : MonoBehaviour
{
    [SerializeField] private DiverController diverController;
    public GameObject shopUI;
    public GameObject openShopText;
    public GameObject closeShopText;
    public GameObject btnAirTube;
    public GameObject btnFillAir;
    public GameObject btnFins;

    public ShopTrigger shopTrigger;
    public bool shopMenuIsOpen = false;

   

    void Start()
    {
        openShopText.SetActive(false);
        closeShopText.SetActive(false);

        btnAirTube.SetActive(false);
        btnFillAir.SetActive(false);
        btnFins.SetActive(false);
    }

    void Update()
    {
        if (shopTrigger.playerInRange && !shopMenuIsOpen)
        {
            openShopText.SetActive(true);
            closeShopText.SetActive(false);
        }

        if (shopMenuIsOpen)
        {
            openShopText.SetActive(false);
            closeShopText.SetActive(true);
        }

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
            OpenShop();
        }
        else if (shopMenuIsOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        Time.timeScale = 0f;
        btnAirTube.SetActive(true);
        btnFillAir.SetActive(true);
        btnFins.SetActive(true);
        shopMenuIsOpen = true;
    }

    public void CloseShop()
    {
        Time.timeScale = 1f;
        btnAirTube.SetActive(false);
        btnFillAir.SetActive(false);
        btnFins.SetActive(false);
        shopMenuIsOpen = false;
    }

    public void BuyUpgrade(int cost, string upgradeType)
    {
        if (CashManager.Instance == null)
        {
            Debug.LogWarning("CashManager saknas.");
            return;
        }

        if (CashManager.Instance.HasEnoughCash(cost))
        {
            CashManager.Instance.SpendCash(cost);

            switch (upgradeType)
            {
                case "Airtube":
                    Debug.Log("Airtube köpt!");
                    break;

                case "Fins":
    Debug.Log("Fins bought");
    if (diverController != null)
    {
        diverController.ActivateFinsBoost(40f, 10f); // 10 sekunder, 40 i speed
    }
    break;

                case "FillAir":
                    Debug.Log("Luft fylld!");
                    AirTimer air = FindAnyObjectByType<AirTimer>();
                    if (air != null)
                    {
                        air.AddAir(5f); // t.ex. 5 sekunder extra luft
                    }
                    break;
            }
        }
        else
        {
            Debug.Log("Not enough money for " + upgradeType);
        }
    }
}




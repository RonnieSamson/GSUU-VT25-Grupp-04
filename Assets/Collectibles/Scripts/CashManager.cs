using UnityEngine;
using TMPro;

public class CashManager : MonoBehaviour
{
    public static CashManager Instance;

    public int cash = 0;
    public TMP_Text cashText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCashUI();
    }

    public void AddCash(int amount)
    {
        cash += amount;
        UpdateCashUI();
    }

    public void SpendCash(int amount)
    {
        cash -= amount;
        UpdateCashUI();
    }

    public bool HasEnoughCash(int amount)
    {
        return cash >= amount;
    }

    public void UpdateCashUI()
    {
        if (cashText != null)
        {
            cashText.text = "Cash: $" + cash.ToString();
        }
    }
}
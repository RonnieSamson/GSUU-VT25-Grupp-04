using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private bool playerInRange = false;
    private float holdTime = 0f;
    
    [Header("Antal pengar per Collectible")]
    [SerializeField] private int cashValue = 10;

    [Header("Antal sekunder för att plocka upp objektet")]
    [SerializeField] private float requiredHoldTime = 2f;
    [SerializeField] private Slider holdSlider;
    [SerializeField] private GameObject instructionText;

    void Start()
    {
        if (holdSlider != null)
        {
            holdSlider.gameObject.SetActive(false);
            holdSlider.value = 0f;
        }

        if (instructionText != null)
        {
            instructionText.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTime += Time.deltaTime;
                holdSlider.value = holdTime / requiredHoldTime;

                if (holdTime >= requiredHoldTime)
                {
                    Collect();
                }
            }
            else
            {
                holdTime = 0f;
                holdSlider.value = 0f;
            }
        }
    }

    void Collect()
    {
        CashManager.Instance.AddCash(cashValue);

        Destroy(gameObject);

        if (holdSlider != null)
        {
            holdSlider.gameObject.SetActive(false);
        }

        if (instructionText != null)
        {
            instructionText.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (holdSlider != null)
            {
                holdSlider.gameObject.SetActive(true);
                holdSlider.value = 0f;
            }

            if (instructionText != null)
            {
                instructionText.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            holdTime = 0f;

            if (holdSlider != null)
            {
                holdSlider.value = 0f;
                holdSlider.gameObject.SetActive(false);
            }

            if (instructionText != null)
            {
                instructionText.SetActive(false);
            }
        }
    }
}

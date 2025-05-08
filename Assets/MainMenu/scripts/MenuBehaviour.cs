using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MenuBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) 
    {
        Debug.Log("Enter");
        
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        transform.localScale = new Vector2(1f, 1f);
    }



public void StartGame()
    {
        Debug.Log("Scene changed to MainScene");
        //SceneManager.LoadScene("MainScene"); Uncomment when MainScene is done
    }

    public void QuitGame()
    {
        Debug.Log("Game quit");
        Application.Quit();
    }
}

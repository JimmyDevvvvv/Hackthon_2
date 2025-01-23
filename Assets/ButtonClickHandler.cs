using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour
{
    // This method will be called when the button is clicked.
    public void ReturnToCoffeeShopHegab()
    {
        // Load the "Coffee_Shop_Hegab" scene
        SceneManager.LoadScene("Coffee_Shop_Hegab");
        Debug.Log("Returning to Coffee_Shop_Hegab scene");
    }
}

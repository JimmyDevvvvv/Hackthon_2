using UnityEngine;
using UnityEngine.SceneManagement; // Import this for scene management

public class SceneManagerController : MonoBehaviour
{
    public void ReturnToCoffeeShopHegab()
    {
        // Load the "Coffee_Shop_Hegab" scene
        SceneManager.LoadScene("CoffeeShop_Http");
        Debug.Log("Returning to Coffee_Shop_Hegab scene");
    }
}

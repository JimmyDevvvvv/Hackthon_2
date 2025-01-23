using UnityEngine;
using UnityEngine.SceneManagement;
public class yarabb : MonoBehaviour
{
    public void ReturnToCoffeeShopHegab()
    {
        // Load the "Coffee_Shop_Hegab" scene
        SceneManager.LoadScene("CoffeeShop_Http");
        Debug.Log("Returning to Coffee_Shop_Hegab scene");
    }
}




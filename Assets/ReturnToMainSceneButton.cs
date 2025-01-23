using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class ReturnToMainSceneButton : MonoBehaviour
{
    public void ReturnToCoffeeShopHegab()
    {
        // Load the "Coffee_Shop_Hegab" scene
        SceneManager.LoadScene("Coffee_Shop_Hegab");
        Debug.Log("Returning to Coffee_Shop_Hegab scene");
    }
}

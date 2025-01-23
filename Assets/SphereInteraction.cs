using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace for scene management

public class SphereInteraction : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad; // The name of the scene to load

    void OnMouseDown()
    {
        // Check if the scene name is provided
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Loading scene: " + sceneToLoad);

            // Use the singleton to load the "Coffee_Shop_Hegab" scene after 10 seconds
            if (SceneManagerController.instance != null)
            {
                SceneManagerController.instance.LoadSceneWithDelay("Coffee_Shop_Hegab", 10f);
            }
            else
            {
                Debug.LogWarning("SceneManagerController instance is not found!");
            }
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned!");
        }
    }
}

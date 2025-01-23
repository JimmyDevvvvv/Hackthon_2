using UnityEngine;
using UnityEngine.SceneManagement; // Import this for scene management

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // The scene name that you want to load

    // This method is called when the button is clicked
    public void OnButtonClick()
    {
        // Check if the scene name is provided
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
            Debug.Log("Loading scene: " + sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned!");
        }
    }
}

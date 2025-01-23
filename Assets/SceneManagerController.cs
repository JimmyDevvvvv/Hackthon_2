using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace for scene management
using System.Collections; // Add this for IEnumerator and coroutines

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController instance;

    void Awake()
    {
        // Ensure only one instance of SceneManagerController exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make sure this object persists across scenes
        }
    }

    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        // Start the coroutine to load the scene after a delay
        StartCoroutine(LoadSceneAfterDelay(sceneName, delay));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the scene
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading scene: " + sceneName);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement; // Add this namespace for scene management
using UnityEngine.UI; // This is for the Button component

public class ButtonInteraction : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad; // The name of the scene to load

    private Button button;

    void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        if (button != null)
        {
            // Attach the OnButtonClick method to the OnClick() event of the button
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("No Button component found on this GameObject!");
        }
    }

    // Method to be called when the button is clicked
    void OnButtonClick()
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

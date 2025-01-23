using UnityEngine;
using UnityEngine.SceneManagement; // This namespace is required for scene management

public class SceneLoaderCollider : MonoBehaviour
{
    // The name of the scene you want to load
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to check if the mouse clicked this collider
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // If the raycast hits this collider, load the scene
                if (hit.collider.gameObject == this.gameObject)
                {
                    if (!string.IsNullOrEmpty(sceneToLoad))
                    {
                        SceneManager.LoadScene(sceneToLoad);
                        Debug.Log("Loading scene: " + sceneToLoad);
                    }
                    else
                    {
                        Debug.LogWarning("Scene name is not assigned!");
                    }
                }
            }
        }
    }
}

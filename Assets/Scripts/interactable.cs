using UnityEngine;

public class interactable : MonoBehaviour
{
    [Header("GameObjects to Toggle")]
    public GameObject objectToEnable; // The object to turn on
    public GameObject objectToDisable; // The object to turn off

    void OnMouseDown()
    {
        // Enable the assigned object
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }

        // Disable the assigned object
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false);
        }
    }
}

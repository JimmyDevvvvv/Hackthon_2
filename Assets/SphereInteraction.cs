using UnityEngine;

public class SphereInteraction : MonoBehaviour
{
    private LineController lineController; // Reference to the LineController

    private void Start()
    {
        // Get the LineController attached to this object
        lineController = GetComponent<LineController>();
    }

    private void OnMouseEnter()
    {
        // When the mouse hovers over the sphere, show the line
        if (lineController != null)
        {
            lineController.ShowLine();
        }
    }

    private void OnMouseExit()
    {
        // When the mouse stops hovering, hide the line
        if (lineController != null)
        {
            lineController.HideLine();
        }
    }
}

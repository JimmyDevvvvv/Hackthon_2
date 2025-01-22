using UnityEngine;

public class LineController : MonoBehaviour
{
    public Transform endPoint; // The sphere (Wi-Fi network)
    public GameObject linePrefab; // Prefab for the LineRenderer

    private LineRenderer lineRenderer; // Reference to the LineRenderer
    private GameObject currentLine;    // Holds the instantiated line

    private void Start()
    {
        // No line is visible at the start
        currentLine = null;
    }

    public void ShowLine()
    {
        // If a line already exists, don't recreate it
        if (currentLine != null) return;

        // Instantiate the line prefab
        currentLine = Instantiate(linePrefab);

        // Get the LineRenderer component
        lineRenderer = currentLine.GetComponent<LineRenderer>();

        // Check if the LineRenderer exists
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is missing in the prefab!");
            return;
        }

        // Set the number of positions in the line
        lineRenderer.positionCount = 2;

        // Set the start and end positions
        UpdateLinePositions();
    }

    public void HideLine()
    {
        // Destroy the existing line
        if (currentLine != null)
        {
            Destroy(currentLine);
            currentLine = null;
        }
    }

    private void UpdateLinePositions()
    {
        if (lineRenderer == null || endPoint == null)
        {
            Debug.LogWarning("LineRenderer or EndPoint is not assigned!");
            return;
        }

        // Set the start position to the camera's position
        lineRenderer.SetPosition(0, Camera.main.transform.position);

        // Set the end position to the sphere's position
        lineRenderer.SetPosition(1, endPoint.position);
    }

    private void Update()
    {
        // Continuously update the line's positions if it exists
        if (currentLine != null)
        {
            UpdateLinePositions();
        }
    }
}

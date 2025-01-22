using UnityEngine;
using TMPro;
using System.Collections;

public class DataTransfer : MonoBehaviour
{
    public TextMeshPro dataText; // The text that will display data
    public Transform startPoint; // Player's device
    public Transform endPoint;   // Wi-Fi network (sphere)
    private float transferSpeed = 2f; // Speed of data transfer
    private string dataString; // The actual data being transferred
    private Color dataColor; // The color of the data (depending on security)
    private LineRenderer lineRenderer; // The LineRenderer component

    private void Start()
    {
        // Set initial position at the player's device
        transform.position = startPoint.position;

        // Get the LineRenderer component from the DataLine prefab
        lineRenderer = GetComponent<LineRenderer>();

        // Set the initial positions of the line
        lineRenderer.SetPosition(0, startPoint.position); // Start at the player's device
        lineRenderer.SetPosition(1, endPoint.position);   // End at the Wi-Fi sphere
    }

    public void SetData(string data, Color color)
    {
        // Set the data string and color for the text
        dataString = data;
        dataColor = color;

        // Apply the color to the text
        dataText.text = dataString;
        dataText.color = dataColor;
    }

    // Start the data transfer animation
    public void StartDataTransfer()
    {
        StartCoroutine(TransferData());
    }

    private IEnumerator TransferData()
    {
        float journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        float startTime = Time.time;

        while (transform.position != endPoint.position)
        {
            // Calculate the distance the data has traveled
            float distanceCovered = (Time.time - startTime) * transferSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            // Move the text from point A to point B
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);

            yield return null;
        }

        // Once data reaches the end point, show educational message
        ShowEducationalMessage();
    }

    private void ShowEducationalMessage()
    {
        string message = dataColor == Color.white ? "This network is open and insecure!" :
                         dataColor == Color.red ? "This network is outdated and vulnerable!" :
                         "This data is securely transferred.";

        Debug.Log(message); // You can also show this message as a UI element or VR popup.
    }
}

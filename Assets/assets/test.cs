using UnityEngine;

public class CameraMouseRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        // Get mouse horizontal movement
        float mouseInput = Input.GetAxis("Mouse X");

        // Rotate the camera based on mouse input
        transform.Rotate(0, mouseInput * rotationSpeed * Time.deltaTime, 0);
    }
}

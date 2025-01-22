using UnityEngine;

public class GrassInfection : MonoBehaviour
{
    [Header("Materials")]
    public Material startMaterial; // Healthy material
    public Material infectedMaterial; // Infected material

    [Header("Settings")]
    public float timeToInfect = 5f; // Time (in seconds) before fully infected

    private Renderer objectRenderer;
    private Material currentMaterial; // Instance for smooth blending
    private float timer = 0f;
    private bool isInfected = false;

    void Start()
    {
        // Get the Renderer and assign the starting material
        objectRenderer = GetComponent<Renderer>();
        if (startMaterial != null)
        {
            // Create a new material instance for blending
            currentMaterial = new Material(startMaterial);
            objectRenderer.material = currentMaterial;
        }
        else
        {
            Debug.LogError($"Start Material is missing on {gameObject.name}");
        }
    }

    void Update()
    {
        if (!isInfected)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Smoothly blend materials over time
            float lerpFactor = Mathf.Clamp01(timer / timeToInfect);
            currentMaterial.Lerp(startMaterial, infectedMaterial, lerpFactor);

            // Check if fully infected
            if (lerpFactor >= 1f)
            {
                CompleteInfection();
            }
        }
    }

    private void CompleteInfection()
    {
        isInfected = true;

        // Ensure the material is fully set to the infected state
        currentMaterial.CopyPropertiesFromMaterial(infectedMaterial);
        Debug.Log($"{gameObject.name} is now fully infected!");
    }
}

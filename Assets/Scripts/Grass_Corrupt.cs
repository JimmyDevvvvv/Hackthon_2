using System.Collections;
using UnityEngine;

public class GrassInfection : MonoBehaviour
{
    [Header("Materials")]
    public Material startMaterial; // Healthy material
    public Material infectedMaterial; // Infected material

    [Header("Transition Settings")]
    public float transitionDuration = 2f; // Time it takes to transition
    public float spreadDelay = 0.5f; // Delay before spreading infection to nearby objects
    public float spreadRadius = 5f; // Radius to infect nearby objects

    private Renderer objectRenderer;
    private Material currentMaterial; // Instance for smooth blending
    private bool isInfected = false;

    void Start()
    {
        // Get the Renderer and assign the starting material
        objectRenderer = GetComponent<Renderer>();
        if (startMaterial != null)
        {
            currentMaterial = new Material(startMaterial);
            objectRenderer.material = currentMaterial;
        }
        else
        {
            Debug.LogError($"Start Material is missing on {gameObject.name}");
        }
    }

    public void Infect()
    {
        if (!isInfected)
        {
            Debug.Log($"{gameObject.name} is now infected!");
            isInfected = true;

            // Start the smooth transition
            StartCoroutine(TransitionToInfected());

            // Start spreading the infection
            StartCoroutine(SpreadInfection());
        }
        else
        {
            Debug.Log($"{gameObject.name} is already infected!");
        }
    }

    private IEnumerator TransitionToInfected()
    {
        Debug.Log($"{gameObject.name} transitioning to infected...");
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpFactor = Mathf.Clamp01(elapsedTime / transitionDuration);

            // Smoothly blend the material properties
            currentMaterial.Lerp(startMaterial, infectedMaterial, lerpFactor);

            Debug.Log($"Lerp Progress on {gameObject.name}: {lerpFactor}");
            yield return null; // Wait for the next frame
        }

        // Ensure the infected material is fully applied at the end
        Debug.Log($"{gameObject.name} fully infected!");
        currentMaterial.CopyPropertiesFromMaterial(infectedMaterial);
    }

    private IEnumerator SpreadInfection()
    {
        yield return new WaitForSeconds(spreadDelay); // Delay before spreading

        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, spreadRadius);
        Debug.Log($"Nearby Objects Found: {nearbyObjects.Length} around {gameObject.name}");

        foreach (Collider col in nearbyObjects)
        {
            GrassInfection nearbyGrass = col.GetComponent<GrassInfection>();
            if (nearbyGrass != null && !nearbyGrass.isInfected)
            {
                Debug.Log($"Infecting {col.gameObject.name} from {gameObject.name}");
                nearbyGrass.Infect();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the spread radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spreadRadius);
    }
}

using UnityEngine;

public class GrassPatch : MonoBehaviour
{
    public Material healthyMaterial; // Green grass
    public Material infectedMaterial; // Red grass
    private Renderer grassRenderer;
    private bool isInfected = false; // Tracks infection state
    private InfectedManager grassManager;
    public float infectionDelay = 5f; // Time before the grass gets infected

    void Start()
    {
        grassRenderer = GetComponent<Renderer>();
        grassRenderer.material = healthyMaterial; // Set the initial material to healthy
        grassManager = FindObjectOfType<InfectedManager>();

        // Start infection process
        Invoke(nameof(StartInfection), infectionDelay);
    }

    public void StartInfection()
    {
        if (!isInfected)
        {
            isInfected = true; // Mark as infected
            grassRenderer.material = infectedMaterial; // Change material to infected
            grassManager.AddInfectedGrass(this); // Notify the manager
        }
    }

    public void HealGrass()
    {
        if (isInfected)
        {
            isInfected = false; // Mark as healthy
            grassRenderer.material = healthyMaterial; // Change material to healthy
            grassManager.RemoveInfectedGrass(this); // Notify the manager
        }
    }

    void OnMouseDown()
    {
        if (isInfected)
        {
            HealGrass(); // Heal the grass if infected
        }
    }
}

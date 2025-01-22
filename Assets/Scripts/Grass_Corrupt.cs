using UnityEngine;

public class GrassPatch : MonoBehaviour
{
    public Material healthyMaterial; // Green grass
    public Material infectedMaterial; // Red grass
    private Renderer grassRenderer;
    private bool isInfected = false; // Tracks infection state
    private InfectedManager grassManager;
    public float infectionDelay = 5f; // Time before the grass gets infected

    [Header("Health Settings")]
    public int maxHealth = 3; // Maximum health
    private int currentHealth; // Current health of the grass

    void Start()
    {
        grassRenderer = GetComponent<Renderer>();
        grassRenderer.material = healthyMaterial; // Set the initial material to healthy
        grassManager = FindObjectOfType<InfectedManager>();

        // Start infection process
        Invoke(nameof(StartInfection), infectionDelay);

        // Initialize health
        currentHealth = maxHealth;
    }

    public void StartInfection()
    {
        if (!isInfected)
        {
            isInfected = true; // Mark as infected
            grassRenderer.material = infectedMaterial; // Change material to infected
            grassManager.AddInfectedGrass(this); // Notify the manager
            currentHealth = maxHealth; // Reset health for healing
        }
    }

    public void HealGrass()
    {
        if (isInfected)
        {
            currentHealth--; // Decrease health with each healing interaction

            if (currentHealth <= 0)
            {
                // Fully healed
                isInfected = false; // Mark as healthy
                grassRenderer.material = healthyMaterial; // Change material to healthy
                grassManager.RemoveInfectedGrass(this); // Notify the manager
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the tag "Healer"
        if (isInfected && other.CompareTag("Healer"))
        {
            HealGrass(); // Heal the grass when the collider interacts
        }
    }
}


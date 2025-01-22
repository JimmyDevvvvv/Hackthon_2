public class GrassPatch : MonoBehaviour
{
    [Header("Materials")]
    public Material healthyMaterial; // Green grass
    public Material infectedMaterial; // Red grass

    [Header("Infection Settings")]
    public float infectionTime = 5f; // Time for the grass to fully turn red
    public bool isInfected = false; // Tracks infection state

    private Renderer grassRenderer;
    private float infectionProgress = 0f; // Progress of infection

    void Start()
    {
        // Get the Renderer component and set the initial material
        grassRenderer = GetComponent<Renderer>();
        if (healthyMaterial != null)
        {
            grassRenderer.material = healthyMaterial;
        }
    }

    void Update()
    {
        // Handle the infection process
        if (isInfected)
        {
            infectionProgress += Time.deltaTime / infectionTime;
            grassRenderer.material.Lerp(healthyMaterial, infectedMaterial, infectionProgress);

            if (infectionProgress >= 1f)
            {
                infectionProgress = 1f; // Clamp progress
            }
        }
    }

    public void StartInfection()
    {
        // Trigger infection process
        if (!isInfected)
        {
            isInfected = true;
            infectionProgress = 0f; // Reset progress
            InfectedManager.Instance.IncreaseInfectedCount();
        }
    }

    public void HealGrass()
    {
        // Heal the grass and reset the infection
        if (isInfected)
        {
            isInfected = false;
            infectionProgress = 0f; // Reset progress
            grassRenderer.material = healthyMaterial;
            InfectedManager.Instance.DecreaseInfectedCount();
        }
    }

    void OnMouseDown()
    {
        // Heal the grass when clicked
        if (isInfected)
        {
            HealGrass();
        }
    }
}

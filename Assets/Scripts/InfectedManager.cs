using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InfectedManager : MonoBehaviour
{
    public TextMeshProUGUI infectedCountText; // Reference to UI Text
    public List<GrassPatch> infectedGrass = new List<GrassPatch>(); // List of infected patches

    private void Start()
    {
        UpdateUI(); // Initialize the UI with the correct count
    }

    public void AddInfectedGrass(GrassPatch grass)
    {
        if (!infectedGrass.Contains(grass))
        {
            infectedGrass.Add(grass);
            UpdateUI();
        }
    }

    public void RemoveInfectedGrass(GrassPatch grass)
    {
        if (infectedGrass.Contains(grass))
        {
            infectedGrass.Remove(grass);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        infectedCountText.text = $"Infected Grass: {infectedGrass.Count}";
    }
}


using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject trustPanel; // The panel with the trust question
    public Text trustMessage; // The message text
    public Button yesButton; // The Yes button
    public Button noButton; // The No button
    public PhishingAttack phishingScript; // Reference to the PhishingAttack script

    public static UIManager Instance; // Singleton pattern for easy access

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Show the trust message panel and set up the buttons
    public void ShowTrustMessage(PhishingAttack script)
    {
        phishingScript = script;
        trustPanel.SetActive(true);
        trustMessage.text = "Do you trust this coin?"; // You can customize this message

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(OnYesClicked);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(OnNoClicked);
    }

    // Handle the Yes button click
    private void OnYesClicked()
    {
        trustPanel.SetActive(false); // Hide the trust panel
        phishingScript.TrapPlayer(); // Trigger the trap logic
    }

    // Handle the No button click
    private void OnNoClicked()
    {
        trustPanel.SetActive(false); // Hide the trust panel
        Debug.Log("You passed the phishing attempt!"); // Optionally show a message or continue
    }
}
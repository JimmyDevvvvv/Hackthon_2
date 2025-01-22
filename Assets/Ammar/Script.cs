using UnityEngine;

public class PhishingAttack : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public Transform goal; // The position where the coin (cheese) is placed
    private bool isTrapped = false; // Flag to check if the player has fallen into a trap

    public AudioClip trapSound; // Sound to play when the player is trapped
    public GameObject trapEffect; // Trap effect (cage slam, etc.)
    public GameObject aiGuide; // The AI guide to explain why the trap worked

    private Rigidbody rb; // Rigidbody for physics-based movement

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    private void Update()
    {
        if (isTrapped)
            return;

        // Handle player movement with WASD (horizontal and vertical axis)
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys

        // Calculate movement direction and apply to the player
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Apply movement to the player using Rigidbody for smoother physics-based movement
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);

        // Rotate player to face the direction they are moving
        if (moveDirection.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Check if the player reaches the goal (coin)
        if (Vector3.Distance(transform.position, goal.position) < 1f)
        {
            HandleCoinPickup();
        }
    }

    // Handle the interaction with the coin
    private void HandleCoinPickup()
    {
        // Show the trust pop-up message
        UIManager.Instance.ShowTrustMessage(this);
    }

    // Trap the player if they pick up a phishing email
    public void TrapPlayer()
    {
        Debug.Log("You’ve been phished! The trap worked because...");
        // Trigger the trap effect (cage slams down, etc.)
        Instantiate(trapEffect, transform.position, Quaternion.identity);

        // Play trap sound
        AudioSource.PlayClipAtPoint(trapSound, transform.position);

        // Show the AI guide to explain the trap
        aiGuide.SetActive(true);

        // Optionally, freeze the player or reset the scene
        isTrapped = true;
    }

    // Call this method to set a trap for a specific coin (cheese)
    public void SetTrap(bool trapStatus)
    {
        isTrapped = trapStatus;
    }
}
using UnityEngine;

public class SphereInteraction : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator objectAnimator; // Reference to the Animator component
    public string animationTriggerName = "Play"; // Name of the trigger to activate

    [Header("GameObject Settings")]
    public GameObject objectToClose; // The GameObject to disable after the animation
    public GameObject objectToEnable; // The GameObject to enable after disabling the first one

    private bool isAnimationPlaying = false; // Tracks if the animation is currently playing

    void OnMouseDown()
    {
        if (objectAnimator != null && objectToClose != null)
        {
            // Play the specified animation trigger
            objectAnimator.SetTrigger(animationTriggerName);
            isAnimationPlaying = true;
            Debug.Log("Playing animation: " + animationTriggerName);
        }
        else
        {
            Debug.LogWarning("Animator or objectToClose is not assigned!");
        }
    }

    void Update()
    {
        if (isAnimationPlaying)
        {
            // Check if the animation is still playing
            if (!objectAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationTriggerName) ||
                objectAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // Animation has finished playing
                isAnimationPlaying = false;

                // Disable the first object
                if (objectToClose != null)
                {
                    objectToClose.SetActive(false);
                    Debug.Log("Object disabled after animation.");
                }

                // Enable the second object
                if (objectToEnable != null)
                {
                    objectToEnable.SetActive(true);
                    Debug.Log("Object enabled after animation.");
                }
            }
        }
    }
}

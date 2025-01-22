using UnityEngine;

public class AudioDialogue : MonoBehaviour
{
    public GameObject Question_Mark; // Visual indicator for interaction availability

    [Header("Audio Clips")]
    public DialogueClip[] dialogueClips; // Array of dialogue clips with conditions

    private AudioSource audioSource;
    private int currentClipIndex = 0; // Tracks the current dialogue clip
    private bool isPlaying = false; // Tracks if an audio clip is currently playing

    [System.Serializable]
    public class DialogueClip
    {
        public AudioClip audioClip; // The audio file
        public bool requiresCondition; // Whether this clip requires a condition to play
        public bool conditionMet; // Tracks if the condition is met
    }

    void Start()
    {
        // Get or add an AudioSource component to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateQuestionMark();
    }

    void OnMouseDown()
    {
        // Only allow interaction if the Question_Mark is active and no audio is playing
        if (Question_Mark != null && Question_Mark.activeSelf && !isPlaying)
        {
            PlayNextClip();
        }
    }

    private void PlayNextClip()
    {
        if (dialogueClips.Length == 0)
        {
            Debug.LogWarning("No dialogue clips assigned.");
            return;
        }

        // Check if there's another clip to play
        if (currentClipIndex < dialogueClips.Length)
        {
            DialogueClip currentClip = dialogueClips[currentClipIndex];

            // If the current clip requires a condition and it's not met, don't play it
            if (currentClip.requiresCondition && !currentClip.conditionMet)
            {
                Debug.Log("Condition not met for the next dialogue.");
                return;
            }

            // Assign and play the current clip
            audioSource.clip = currentClip.audioClip;
            audioSource.Play();

            // Mark that audio is playing
            isPlaying = true;

            // Disable interaction (turn off Question_Mark)
            if (Question_Mark != null)
            {
                Question_Mark.SetActive(false);
            }

            // Wait for the audio to finish before re-enabling interaction
            Invoke(nameof(OnClipEnd), audioSource.clip.length);

            // Move to the next clip for subsequent clicks
            currentClipIndex++;
        }
        else
        {
            Debug.Log("All dialogue clips have been played.");
        }
    }

    private void OnClipEnd()
    {
        // Allow interaction again after the clip ends
        isPlaying = false;

        // Update the Question_Mark based on the next clip's condition
        UpdateQuestionMark();
    }

    private void UpdateQuestionMark()
    {
        // Check if the next clip is available and whether it requires a condition
        if (currentClipIndex < dialogueClips.Length)
        {
            DialogueClip nextClip = dialogueClips[currentClipIndex];

            if (nextClip.requiresCondition && !nextClip.conditionMet)
            {
                // Hide the Question_Mark if the condition is not met
                if (Question_Mark != null)
                {
                    Question_Mark.SetActive(false);
                }
            }
            else
            {
                // Show the Question_Mark if no condition is required or if the condition is met
                if (Question_Mark != null)
                {
                    Question_Mark.SetActive(true);
                }
            }
        }
        else
        {
            // Hide the Question_Mark if there are no more clips
            if (Question_Mark != null)
            {
                Question_Mark.SetActive(false);
            }
        }
    }

    // Method to manually set a condition as met
    public void SetConditionMet(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < dialogueClips.Length)
        {
            dialogueClips[clipIndex].conditionMet = true;
            UpdateQuestionMark(); // Re-evaluate Question_Mark visibility
        }
    }
}

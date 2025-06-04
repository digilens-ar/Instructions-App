using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class StepManager : MonoBehaviour
{
    [System.Serializable]
    public class Step
    {
        public string stepTitle;
        public string videoTitle;
        public string stepDescription;
        public VideoClip videoClip;
    }

    // List of steps
    public List<Step> steps = new List<Step>();
    private int currentStepIndex = 0;

    // UI Elements (Using TMP)
    public TMP_Text stepTitleTMP; // Title for the instruction panel
    public TMP_Text videoTitleTMP; // Title for the video panel
    public TMP_Text stepDescriptionTMP; // Instruction text description
    public VideoPlayer videoPlayer; // Video Player
    public GameObject backButton; //Back Button GameObject
    public GameObject nextButton; //Next Button GameObject

    void Start()
    {
        UpdateStep(); // Initialize first step
    }

    // Button function to go to the next step
    public void NextStep()
    {
        if (currentStepIndex < steps.Count - 1)
        {
            currentStepIndex++;
            UpdateStep();
        }
    }

    // Button function to go to the previous step
    public void PreviousStep()
    {
        if (currentStepIndex > 0)
        {
            currentStepIndex--;
            UpdateStep();
        }
    }

    private void UpdateStep()
    {
        // Update TextMeshPro elements
        stepTitleTMP.text = steps[currentStepIndex].stepTitle;
        videoTitleTMP.text = steps[currentStepIndex].videoTitle;
        stepDescriptionTMP.text = steps[currentStepIndex].stepDescription;

        // Update Video
        videoPlayer.clip = steps[currentStepIndex].videoClip;
        videoPlayer.Play();

        // Hide "Back" button if on the first step
        backButton.SetActive(currentStepIndex > 0);

        // Hide "Next" button if on the last step
        nextButton.SetActive(currentStepIndex < steps.Count - 1);
    }
}

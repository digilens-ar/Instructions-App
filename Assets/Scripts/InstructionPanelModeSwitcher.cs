using UnityEngine;
using TMPro;
using QCHT.Interactions.Distal.ControlBox;

public class InstructionPanelModeSwitcher : MonoBehaviour
{
    // Pinned and uninned modes
    public enum PanelMode { Pin, Unpin }
    public PanelMode currentMode = PanelMode.Pin;

    public GameObject instructionPanel; // Reference to the instruction panel parent
    public TextMeshProUGUI modeButtonText; // Reference to the mode-switching button
    public Transform userTransform; // Reference to the user's camera location
    public GameObject resetButton; // Reference to the button that reset the instruction panel location
    public float resetDistance = 2f; // How far in front of user the panel resets
    public float resetHeightOffset = 0f; // Optional vertical offset for reset position

    // Internal state to track if the panel is currently pinned
    private bool isPinned = false;
    private Vector3 pinnedPosition;

    // Reference to the QCHT control box for hand tracking interaction
    private QCHTControlBox controlBox;

    void Start()
    {
        // Cache the QCHTControlBox component from the instruction panel
        controlBox = instructionPanel.GetComponent<QCHTControlBox>();
        if (controlBox == null)
        {
            Debug.LogWarning("QCHTControlBox component not found on the instruction panel.");
        }

        // Initialize panel based on the starting mode
        if (currentMode == PanelMode.Unpin)
        {
            isPinned = false;
            if (controlBox != null)
                controlBox.enabled = true;
            if (resetButton != null)
                resetButton.SetActive(true);
            modeButtonText.text = "Pin";
        }
        else // default to pinned mode
        {
            currentMode = PanelMode.Pin;
            isPinned = true;
            pinnedPosition = instructionPanel.transform.position;
            if (controlBox != null)
                controlBox.enabled = false;
            if (resetButton != null)
                resetButton.SetActive(false);
            modeButtonText.text = "Unpin";
        }
    }

    void Update()
    {
        // Keep the panel fixed in place if it is pinned
        if (isPinned)
        {
            instructionPanel.transform.position = pinnedPosition;
        }
    }

    public void CycleMode()
    {
        // Toggle between pinned and unpinned modes
        if (currentMode == PanelMode.Pin)
            currentMode = PanelMode.Unpin;
        else
            currentMode = PanelMode.Pin;

        // Update panel behavior based on new mode
        switch (currentMode)
        {
            case PanelMode.Pin:
                isPinned = true;
                pinnedPosition = instructionPanel.transform.position;

                if (controlBox != null)
                    controlBox.enabled = false;

                if (resetButton != null)
                    resetButton.SetActive(false);

                modeButtonText.text = "Unpin"; // Show the next possible mode
                break;

            case PanelMode.Unpin:
                isPinned = false;

                if (controlBox != null)
                    controlBox.enabled = true;

                if (resetButton != null)
                    resetButton.SetActive(true);

                modeButtonText.text = "Pin"; // Show the next possible mode
                break;
        }
    }

    public void ResetPanelPosition()
    {
        // Safety check: ensure user reference is valid
        if (userTransform == null) return;

        // Calculate a position in front of the user at a fixed height
        Vector3 forward = new Vector3(userTransform.forward.x, 0, userTransform.forward.z).normalized;
        Vector3 targetPosition = userTransform.position + forward * resetDistance;
        targetPosition.y = userTransform.position.y + resetHeightOffset;

        // Move panel to the calculated position
        instructionPanel.transform.position = targetPosition;

        // Rotate the panel to face the user
        Vector3 lookAtPos = new Vector3(userTransform.position.x, targetPosition.y, userTransform.position.z);
        instructionPanel.transform.LookAt(lookAtPos);
        instructionPanel.transform.Rotate(0, 180, 0);
    }
}

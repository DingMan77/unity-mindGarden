using UnityEngine;

public class InstructionPopup : MonoBehaviour
{
    public GameObject instructionPopup;

    void Start()
    {
        // Hide the instruction popup at the beginning
        instructionPopup.SetActive(false);
    }

    public void ToggleInstructionPopup()
    {
        // Toggle the visibility of the instruction popup
        instructionPopup.SetActive(!instructionPopup.activeSelf);
    }

    void Update()
    {
        // Check if the instruction popup is active and the player clicks outside of it
        if (instructionPopup.activeSelf && Input.GetMouseButtonDown(0))
        {
            // Get the position of the mouse click
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the click position is outside of the instruction popup
            if (!instructionPopup.GetComponent<RectTransform>().rect.Contains(clickPosition))
            {
                // Hide the instruction popup
                instructionPopup.SetActive(false);
            }
        }
    }
}

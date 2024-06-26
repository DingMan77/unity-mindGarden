using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

//[System.Serializable] // This attribute allows the class to show up in the Unity Inspector
// public class DialoguePiece
// {
//     public string dialogueText;
//     public string[] responses;
// }

public class DialogueManager_panda : MonoBehaviour
{
    public GameObject dialoguePanelPrefab;
    public Transform dialoguePanelContainer; // Assign a panel with Vertical Layout Group
    public ScrollRect scrollRect;
    public DialoguePiece[] dialoguePieces;
    private int currentDialogueIndex = 0;

    private List<GameObject> activeDialoguePanels = new List<GameObject>();
    public Canvas canvas;
    void Start()
    {
        ShowDialogue(currentDialogueIndex);
    }

    public void OnOptionSelected(int optionIndex, Button[] responseButtons, GameObject currentPanel)
    {
        // Optionally, do something with the optionIndex

        // Load next dialogue piece
        foreach (var btn in responseButtons)
        {
            btn.interactable = false;
        }

        if (dialoguePieces[currentDialogueIndex].isBranchingDialogue)
        {
            // This is a branching dialogue piece
            HandleBranchingDialogue(optionIndex);
        }
        else
        {
            // Regular dialogue flow
            ++currentDialogueIndex;
            ShowDialogue(currentDialogueIndex);
        }
    }

    private void HandleBranchingDialogue(int optionIndex)
    {
        // Assuming the first option continues the dialogue and the second option starts the game
        if (optionIndex == 0) // "Sure, tell me more"
        {
            ++currentDialogueIndex; // Move to the next dialogue piece
            ShowDialogue(currentDialogueIndex);
        }
        else if (optionIndex == 1) // "I'm ready to play"
        {
            // Start the game immediately
            GameManager_Panda.Instance.StartGame();
        }
    }

    IEnumerator ShowResponseButtonsWithDelay(Button[] responseButtons, DialoguePiece piece, GameObject panelInstance)
    {
        foreach (var button in responseButtons)
        {
            button.gameObject.SetActive(false);
        }
        Debug.Log("hahahahha");
        yield return new WaitForSeconds(10f); // Wait for 10 seconds
        Debug.Log("dkshdkskjd");
        // After waiting, now set up the buttons
        for (int i = 0; i < responseButtons.Length; i++)
        {
            if (i < piece.responses.Length)
            {
                responseButtons[i].gameObject.SetActive(true);
                responseButtons[i].GetComponentInChildren<Text>().text = piece.responses[i];
                int optionIndex = i;
                responseButtons[i].onClick.RemoveAllListeners();
                responseButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex, responseButtons, panelInstance));
                Debug.Log("Button " + i + " activated after delay");
            }
            else
            {
                responseButtons[i].gameObject.SetActive(false);
            }
        }

    }

    void ShowDialogue(int index)
    {
        if (index < dialoguePieces.Length)
        {
            DialoguePiece piece = dialoguePieces[index];
            GameObject panelInstance = Instantiate(dialoguePanelPrefab, dialoguePanelContainer);
            LayoutRebuilder.ForceRebuildLayoutImmediate(dialoguePanelContainer.GetComponent<RectTransform>());
            panelInstance.SetActive(true);

            Transform systemOutputPanel = panelInstance.transform.Find("systemOutput");
            Transform userInputPanel = panelInstance.transform.Find("userInput");

            // Set the dialogue text
            Text dialogueTextComponent = systemOutputPanel.Find("content").GetComponentInChildren<Text>();
            dialogueTextComponent.text = piece.dialogueText;

            activeDialoguePanels.Add(panelInstance);

            // Set up response buttons
            Button[] responseButtons = userInputPanel.GetComponentsInChildren<Button>(true);
            Debug.Log("Found " + responseButtons.Length + " response buttons.");
            if (piece.isSharingDialogue)
            {
                StartCoroutine(ShowResponseButtonsWithDelay(responseButtons, piece, panelInstance)); // Now passing the parameters
            }
            else
            {
                for (int i = 0; i < responseButtons.Length; i++)
                {
                    if (i < piece.responses.Length)
                    {
                        responseButtons[i].gameObject.SetActive(true);
                        responseButtons[i].GetComponentInChildren<Text>().text = piece.responses[i];
                        int optionIndex = i;
                        responseButtons[i].onClick.RemoveAllListeners();
                        responseButtons[i].onClick.AddListener(() => OnOptionSelected(optionIndex, responseButtons, panelInstance));
                        //responseButtons[i].onClick.AddListener(() => ShowDialogue(++currentDialogueIndex));

                        // Debugging to confirm listeners are added
                        Debug.Log("Added listener to button " + i);
                    }
                    else
                    {
                        responseButtons[i].gameObject.SetActive(false);
                    }
                }
            }
            
            // Scroll to the bottom to show the latest dialogue
            StartCoroutine(ScrollToBottom());
        }
        else
        {
            GameManager_Panda.Instance.StartGame(); // Notify GameManager to start the game
        }
    }

    IEnumerator ScrollToBottom()
    {
        // Wait for the end of the frame to ensure all UI layout has been updated
        yield return new WaitForEndOfFrame();

        // Scroll to the bottom
        scrollRect.verticalNormalizedPosition = 0f;
    }

    

    public void ClearDialogue()
    {
        foreach (var panel in activeDialoguePanels)
        {
            Destroy(panel);
        }
        activeDialoguePanels.Clear();
    }
}

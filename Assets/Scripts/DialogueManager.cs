using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{ 
    [Header("UI Elements")]
    public GameObject dBox;
    public TextMeshProUGUI dText;

    public bool dialogActive;

    [Header("Dialogue Lines")]
    public string[] dialogLines;
    public int currentLine;

    private PlayerMovement player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        dBox.SetActive(false);
    }

    void Update()
    {
        if (dialogActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }
        if(currentLine >= dialogLines.Length)
        {
            dBox.SetActive(false);
            dialogActive = false;

            currentLine = 0;
            player.canMove = true;
        }
        dText.text = dialogLines[currentLine];
    }

    public void ShowDialogue()
    {
        dialogActive = true;
        dBox.SetActive(true);
        player.canMove = false;
    }
}

using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.03f;

    private string[] currentLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private bool dialogueActive = false;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
                SkipTyping();
            else
                NextLine();
        }
    }

    public void StartDialogue(string[] lines)
    {
        Debug.Log($"StartDialogue llamado desde: {gameObject.name}, dialogueActive: {dialogueActive}");
        if (dialogueActive) return;

        currentLines = lines;
        currentLineIndex = 0;
        dialogueActive = true;
        Time.timeScale = 0f;
        dialoguePanel.SetActive(true);
        ShowLine(currentLines[currentLineIndex]);
    }

    private void ShowLine(string line)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    private void SkipTyping()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialogueText.text = currentLines[currentLineIndex];
        isTyping = false;
    }

    private void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < currentLines.Length)
            ShowLine(currentLines[currentLineIndex]);
        else
            EndDialogue();
    }

    private void EndDialogue()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        Time.timeScale = 1f;
    }
}
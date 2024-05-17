using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTetrisScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed;
    public float delay;
    int index;

    public GameObject panel;
    public bool panelMostrado;

    public UnityEvent OnDialogueEnd; // Evento para indicar que el diálogo ha terminado

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(delay);

        NextLine();
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            EsconderPanel();
        }
    }

    public void MostrarPanel()
    {
        panel.SetActive(true);
        panelMostrado = true;
    }

    public void EsconderPanel()
    {
        panel.SetActive(false);
        panelMostrado = false;
        OnDialogueEnd.Invoke(); // Invocar el evento cuando el diálogo ha terminado
    }
}

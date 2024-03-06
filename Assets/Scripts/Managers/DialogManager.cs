using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
  public GameObject dialoguePanel;
  public TextMeshProUGUI dialogueText;
  public TextMeshProUGUI nextDialogueText;
  public string[] dialogue;
  private int index;

  public float wordSpeed;
  private bool isPlayerOnTrigger;

  // Update is called once per frame
  void Update()
  {
    if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.C))
    {
      NextLine();
    }
  }

  public void ZeroText()
  {
    dialogueText.text = "";
    index = 0;
    dialoguePanel.SetActive(false);
    nextDialogueText.enabled = false;
  }

  public void NextLine()
  {
    if (index < dialogue.Length - 1)
    {
      index++;
      dialogueText.text = "";
      if (!(index < dialogue.Length - 1)) nextDialogueText.enabled = false;
      StartCoroutine(Typing());
    }
  }

  IEnumerator Typing()
  {
    foreach (char letter in dialogue[index].ToCharArray())
    {
      if (!isPlayerOnTrigger) yield break;

      dialogueText.text += letter;
      yield return new WaitForSeconds(wordSpeed);
    }
  }

  void OnTriggerEnter2D(Collider2D others)
  {
    if (others.CompareTag("Player"))
    {
      dialoguePanel.SetActive(true);
      isPlayerOnTrigger = true;
      if (dialogue.Length > 1)
      {
        Debug.Log(dialogue.Length);
        nextDialogueText.enabled = true;
      }
      StartCoroutine(Typing());
    }
  }

  void OnTriggerExit2D(Collider2D others)
  {
    if (others.CompareTag("Player"))
    {
      isPlayerOnTrigger = false;
      ZeroText();
    }
  }
}

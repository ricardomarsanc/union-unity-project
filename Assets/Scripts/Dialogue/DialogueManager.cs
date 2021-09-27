using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Animator animator;

    private Queue<string> sentences;

    public Text nameText;
    public Text dialogueText;

    public bool isDialogueActive = false;
    public bool talkedToHighPriest = false;

    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        isDialogueActive = true;
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        if (talkedToHighPriest) { 
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        }
        else
        {
            if (dialogue.name == "High Priest")
            {
                talkedToHighPriest = true;

                foreach (string sentence in dialogue.sentences)
                {
                    sentences.Enqueue(sentence);
                }
            }
            else if (dialogue.name == "Milo")
            {
                foreach (string sentence in dialogue.sentences)
                {
                    sentences.Enqueue(sentence);
                }
                DisplayNextSentence();
            }
            else if (dialogue.name == "Wallace")
            {
                sentences.Enqueue("Ahhhh! Who are you? I’ve never seen you before. Newcomers need to report to the high priest immediately! He is in our temple up the hill, you have to go there and talk to him.  NOW!");
            }
            else
            {
                sentences.Enqueue("You need to talk with the high priest as soon as possible or you will feel his wrath.");
            }
        }

        // DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        animator.SetBool("isOpen", false);
    }
}

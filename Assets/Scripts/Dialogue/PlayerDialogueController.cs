using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueController : MonoBehaviour
{

    public GameObject textObject;
    public DialogueManager dialogueManager;
    private Dialogue dialogue;
    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if(dialogueManager == null)
            dialogueManager = FindObjectOfType<DialogueManager>();
        if (textObject == null)
            textObject = gm.UI_TalkText;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            textObject.SetActive(true);
            dialogue = other.gameObject.GetComponent<DialogueTrigger>().dialogue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            dialogue = null;
            textObject.SetActive(false); 
        }
    }

    private void Update()
    {

        if (dialogueManager.isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.E)) {
                dialogueManager.DisplayNextSentence();
            }
            return;
        }
        else
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerDash>().enabled = true;

            if (Input.GetKeyDown(KeyCode.E) && dialogue != null)
            {
                dialogueManager.StartDialogue(dialogue);
                dialogueManager.DisplayNextSentence();

                textObject.SetActive(false);

                GetComponent<PlayerController>().enabled = false;
                GetComponent<PlayerDash>().enabled = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerDialogueController : MonoBehaviour
{

    public GameObject textObject;
    public DialogueManager dialogueManager;
    private Dialogue dialogue;
    private GameManager gm;
    private GameObject currentNPC;

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
            currentNPC = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            dialogue = null;
            currentNPC = null;
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
            currentNPC.GetComponent<NavMeshAgent>().isStopped = false;

            if (Input.GetKeyDown(KeyCode.E) && dialogue != null)
            {
                currentNPC.GetComponent<NavMeshAgent>().isStopped = true;

                dialogueManager.StartDialogue(dialogue);
                dialogueManager.DisplayNextSentence();

                textObject.SetActive(false);

                GetComponent<PlayerController>().enabled = false;
                GetComponent<PlayerDash>().enabled = false;
            }
        }
    }
}

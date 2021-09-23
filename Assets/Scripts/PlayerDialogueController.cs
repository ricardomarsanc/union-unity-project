using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueController : MonoBehaviour
{

    public GameObject textObject;
    public DialogueManager dialogueManager;
    public Dialogue dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            textObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            textObject.SetActive(false);
            dialogue = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && textObject.activeSelf)
        {
            if(dialogue != null)
            {
                dialogueManager.StartDialogue(dialogue);
            }

            Debug.Log("Hablamos");
        }
    }
}

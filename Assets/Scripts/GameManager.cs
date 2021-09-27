using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject UI_TalkText;
    public GameObject UI_GrabItemText;
    public InventoryObject PlayerInventory;
    public GameObject player;
    public DialogueTrigger[] playerDialogues;

    public GameManager instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // If removed, the items of the inventory will remain
        PlayerInventory.Clear();
    }

    private void Start()
    {
        playerDialogues = player.GetComponents<DialogueTrigger>();

        foreach(DialogueTrigger dt in playerDialogues)
        {
            if(dt.dialogueOrder == 0)
            {
                StartCoroutine(DisplayDialogue(dt, 7f));
            }
            else if(dt.dialogueOrder == 1)
            {
                StartCoroutine(DisplayDialogue(dt, 20f));
            }
            else
            {
                StartCoroutine(DisplayDialogue(dt, 35f));
            }
        }        
    }

    private void Update()
    {
        if (!FindObjectOfType<DialogueManager>().isDialogueActive)
        {
            player.GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerDash>().enabled = true;
        }
    }

    private IEnumerator DisplayDialogue(DialogueTrigger dt, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        dt.TriggerDialogue();
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerDash>().enabled = false;
        player.GetComponent<Animator>().speed = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;
    private void Start() {
        player = GameObject.Find("Player");
    }
    private void Update() {
        if(player.GetComponent<Interaction>().canInteract && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
     
}

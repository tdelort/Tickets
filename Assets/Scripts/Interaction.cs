using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{  
    public bool isInteracting;
    public GameObject interractedPassenger;

    public Passenger closestPassenger;

    public DialogueManager dialogueManager;

    public Animator animator;


    [SerializeField]
    private PassengerControls passengerControls;

    private void Awake()
    {
    }
    void Start()
    {
        isInteracting = false;
    }

    public bool canInteract()
    {
        return closestPassenger != null;
    }

    // Update is called once per frame
    void Update()
    {

        if(isInteracting)
        {
            if(Input.GetButtonDown("Interact") || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f)
            {
                Debug.Log("Leave Interact");
                EndInteract();
                isInteracting = false;
            }
        }
        else
        {
            if(closestPassenger!=null && !closestPassenger.hasBeenFined && Input.GetButtonDown("Interact"))
            {
                Debug.Log("Interact");
                isInteracting = true;
                Interact();
            }    
            else if (Input.GetButtonDown("Next Sentence"))
            {
                Debug.Log("Next Sentence");
                dialogueManager.displayNextSentence();
            }
        }

    }
    //This can be optimized, but for now it works just fine
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (closestPassenger == null)
        {
            setClosestPassenger(collision.gameObject.GetComponent<Passenger>());
        }
        else if (closestPassenger != collision.gameObject)
        {
            float distToBeat = Vector3.Distance(this.transform.position,
                closestPassenger.transform.position);
            float dist = Vector3.Distance(this.transform.position,
                collision.transform.position);
            if (dist < distToBeat)
            {
                setClosestPassenger(collision.gameObject.GetComponent<Passenger>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        setClosestPassenger(null);
    }

    private void setClosestPassenger(Passenger passenger)
    {
        if(passenger != null && passenger.hasBeenFined)
            return;
        if (closestPassenger != null)
        {
            closestPassenger.transform.GetChild(0).gameObject.SetActive(false);
        }
        closestPassenger = passenger;
        if (closestPassenger != null)
        {
            closestPassenger.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void Interact()
    {
        animator.SetTrigger("Interact");
        Passenger interractedPassenger = closestPassenger.GetComponent<Passenger>();
        if(!interractedPassenger.isSpecial())
        {
            dialogueManager.StartDialogue(interractedPassenger.dialogue);
            passengerControls.gameObject.SetActive(true);
            passengerControls.Set(interractedPassenger);
        }
        else
        {
            //cast passenger to special passenger
            SpecialPassenger spassenger = interractedPassenger as SpecialPassenger;
            spassenger.canMove = false;
            dialogueManager.StartSpecialDialogue(spassenger.dialogue);
        }
    }

    public void EndInteract()
    {
        Debug.Log("Ending interaction");
        passengerControls.gameObject.SetActive(false);
        dialogueManager.EndDialogue();
    }
}

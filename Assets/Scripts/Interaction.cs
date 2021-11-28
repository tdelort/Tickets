using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{  
    public bool isInteracting;

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

        //Clic pour intéragir

        if(closestPassenger!=null && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }    
        else if(isInteracting && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            EndInteract();
        }
        //debug purpose only, the pause menu will be there
        else if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("LevelSelection");
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
        isInteracting = true;
        animator.SetTrigger("Interact");
        Passenger passenger = closestPassenger.GetComponent<Passenger>();
        if(!passenger.isSpecial())
        {
            dialogueManager.StartDialogue(passenger.dialogue);
            passengerControls.gameObject.SetActive(true);
            passengerControls.Set(passenger);
        }
        else
        {
            //cast passenger to special passenger
            SpecialPassenger spassenger = passenger as SpecialPassenger;
            spassenger.canMove = false;
            dialogueManager.StartSpecialDialogue(spassenger.dialogue);
        }
    }

    public void EndInteract()
    {
        passengerControls.gameObject.SetActive(false);
        isInteracting = false;
        dialogueManager.EndDialogue();
    }
}

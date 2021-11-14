using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{  
    public bool isInteracting;

    private GameObject closestPassenger;

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

    // Update is called once per frame
    void Update()
    {

        //Clic pour intéragir

        if(closestPassenger!=null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Interact());
        }    
        else if(isInteracting && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            passengerControls.gameObject.SetActive(false);
            isInteracting = false;
            dialogueManager.EndDialogue();

        }
        //debug purpose only, the pause menu will be there
        else if ((Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneManager.LoadScene("LevelSelection");
        }


    }
    //This can be optimisable, but for now it works just fine
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (closestPassenger == null)
        {
            setClosestPassenger(collision.gameObject);
        }
        else if (closestPassenger != collision.gameObject)
        {
            float distToBeat = Vector3.Distance(this.transform.position,
                closestPassenger.transform.position);
            float dist = Vector3.Distance(this.transform.position,
                collision.transform.position);
            if (dist < distToBeat)
            {
                setClosestPassenger(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (closestPassenger.Equals(collision.gameObject))
        {
            setClosestPassenger(null);
        }
    }

    private void setClosestPassenger(GameObject passenger)
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

    IEnumerator Interact()
    {
        isInteracting = true;
        animator.SetTrigger("Interact");
        Debug.Log("Test started");

        yield return new WaitForSeconds(1f);
        
        passengerControls.Set(closestPassenger.GetComponent<Passenger>());
        passengerControls.gameObject.SetActive(true);

    }
}

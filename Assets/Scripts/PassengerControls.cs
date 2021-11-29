using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerControls : MonoBehaviour
{
    Passenger passenger;

    public DialogueManager dialogueManager;
    public enum ControlObjectType
    {
        TICKET,
        SUBSCRIPTION,
        PASSEPORT,
        ID,
        RULELIST,
        FINEMACHINE,
        WATCH
    }

    [System.Serializable]
    public struct ControlObject
    {
        public ControlObjectType type;
        public GameObject go;
        public uint layer;
    }
    
    public enum FineMachineState
    {
        IDLE,
        FINE_SELECTED
    }

    [SerializeField] private List<ControlObject> controlObjects;
    private GameObject watch;
    private DateTime startTime;
    private TimeSpan difference;
    [SerializeField] private float originalZ = 0;

    private int score = 0;

    RaycastHit2D hit;

    bool fine = false;

    private void Start()
    {
        startTime = DateTime.Now;
        foreach (ControlObject co in controlObjects)
        { 
            if (co.type == ControlObjectType.WATCH)
            {
                watch = co.go;
            }
        }
    }

    void Update()
    {
        //update time on watch, can be optimized, I guess, but for now it is just fine
        difference = DateTime.Now - startTime ;
        DateTime processedTime = GameManager.GameTime.AddSeconds(difference.TotalSeconds);
        watch.GetComponentInChildren<TextMesh>().text = processedTime.ToString("G");

        //check input
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if(hit.collider != null)
            {   
                if(hit.collider.gameObject.CompareTag("FineButton"))
                {
                    Debug.Log("Hit fine button");
                    SetFineMachine(hit.collider.transform.parent.gameObject, FineMachineState.FINE_SELECTED);
                    fine = true;
                    passenger.dialogue.SetDialogue(Sentence.SentenceType.PLEAD);
                    dialogueManager.StartDialogue(passenger.dialogue);
                }
                
                if(fine && hit.collider.gameObject.CompareTag("ValidateButton"))
                {
                    Debug.Log("FINE VALIDATED");
                    SetFineMachine(hit.collider.transform.parent.gameObject, FineMachineState.IDLE);
                    // Add acion to fine the passenger here
                    fine = false;
                    GameManager.CheckUsagerWhenAmendeClicked(passenger);
                    GameObject.FindObjectOfType<Interaction>().EndInteract();
                    dialogueManager.StartDialogue(passenger.dialogue);
                }

                if(fine && hit.collider.gameObject.CompareTag("CancelButton"))
                {
                    Debug.Log("FINE CANCELLED");
                    SetFineMachine(hit.collider.transform.parent.gameObject, FineMachineState.IDLE);
                    fine = false;
                }
            }
        }
    }

    public void Set(Passenger p)
    {
        if(p == null)
        {
            Debug.LogError("Passenger is null");
            return;
        }
        originalZ = transform.position.z;
        passenger = p;
        foreach(ControlObject co in controlObjects)
        {
            switch(co.type)
            {
                case ControlObjectType.TICKET:
                    SetTicket(co.go, p.ticket);
                    break;
                case ControlObjectType.SUBSCRIPTION:
                    SetSubscription(co.go, p.subscription);
                    break;
                case ControlObjectType.PASSEPORT:
                    SetPasseport(co.go, p.passeport);
                    break;
                case ControlObjectType.ID:
                    SetId(co.go, p.id);
                    break;
                case ControlObjectType.RULELIST:
                    SetRuleList(co.go, new List<string>(){"Tickets are \n Mandatory", "No smoking", "No alcohol"});
                    break;
                case ControlObjectType.FINEMACHINE:
                    SetFineMachine(co.go, FineMachineState.IDLE);
                    break;
            } 
            co.go.transform.position = new Vector3(co.go.transform.position.x, co.go.transform.position.y, originalZ);
            co.go.transform.position -= Vector3.forward * co.layer * 0.1f;
        } //foreach
    }

    /*
     * Functions to set the objects using the passenger's data
     */
    private void SetTicket(GameObject obj, Passenger.Ticket t)
    {
        if(!t.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.GetComponentInChildren<TextMesh>().text = t.ToText();
    }

    private void SetSubscription(GameObject obj, Passenger.Subscription s)
    {
        if (!s.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.GetComponentInChildren<TextMesh>().text = s.ToText();
    }
    private void SetId(GameObject obj, Passenger.ID i)
    {
        if (!i.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.GetComponentInChildren<TextMesh>().text = i.ToText();
    }
    private void SetPasseport(GameObject obj, Passenger.Passeport p)
    {
        if(!p.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.GetComponentInChildren<TextMesh>().text = p.ToText();
    }

    private void SetRuleList(GameObject obj, List<string> rules)
    {
        obj.SetActive(true);
        obj.GetComponentInChildren<TextMesh>().text = "";
        foreach(string s in rules)
        {
            obj.GetComponentInChildren<TextMesh>().text += "- " + s + "\n";
        }
    }

    private void SetFineMachine(GameObject obj, FineMachineState state)
    {
        switch(state)
        {
            case FineMachineState.IDLE:
                obj.GetComponentInChildren<TextMesh>().text = "Fine ?";
                break;
            case FineMachineState.FINE_SELECTED:
                obj.GetComponentInChildren<TextMesh>().text = "Validate/Cancel ?";
                break;
        }
    }

}

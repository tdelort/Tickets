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
    private GameObject fineMachine;
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
                    //Debug.Log("Hit fine button");
                    SetFineMachine(fineMachine, FineMachineState.FINE_SELECTED);
                    fine = true;
                    passenger.dialogue.SetDialogue(Sentence.SentenceType.PLEAD);
                    dialogueManager.StartDialogue(passenger.dialogue);
                }
                
                if(fine && hit.collider.gameObject.CompareTag("ValidateButton"))
                {
                    //Debug.Log("FINE VALIDATED");
                    SetFineMachine(fineMachine, FineMachineState.IDLE);
                    // Add acion to fine the passenger here
                    fine = false;
                    
                    if(passenger != null)
                        GameManager.CheckUsagerWhenAmendeClicked(passenger);
                    passenger.hasBeenFined = true;
                    passenger.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.FindObjectOfType<Interaction>().EndInteract();
                    dialogueManager.StartDialogue(passenger.dialogue);
                }

                if(fine && hit.collider.gameObject.CompareTag("CancelButton"))
                {
                    //Debug.Log("FINE CANCELLED");
                    SetFineMachine(fineMachine, FineMachineState.IDLE);
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
                    List<string> rules = new List<string>(){
                        "Titre de transport",
                        "Pièce d'identité",
                        "",
                    };
                    SetRuleList(co.go, rules);
                    break;
                case ControlObjectType.FINEMACHINE:
                    fineMachine = co.go;
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
        obj.transform.GetChild(3).GetChild(1).GetComponentInChildren<TextMesh>().text = t.ToText();
    }

    private void SetSubscription(GameObject obj, Passenger.Subscription s)
    {
        if (!s.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.transform.GetChild(6).GetChild(1).GetComponentInChildren<TextMesh>().text = s.NameToText();
        obj.transform.GetChild(1).GetChild(1).GetComponentInChildren<TextMesh>().text = s.DelToText();
        obj.transform.GetChild(3).GetChild(1).GetComponentInChildren<TextMesh>().text = s.ValToText();
    }
    private void SetId(GameObject obj, Passenger.ID i)
    {
        if (!i.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.transform.GetChild(1).GetChild(1).GetComponentInChildren<TextMesh>().text = i.name;
        obj.transform.GetChild(2).GetChild(1).GetComponentInChildren<TextMesh>().text = i.surname;
        obj.transform.GetChild(4).GetChild(1).GetComponentInChildren<TextMesh>().text = i.birthToText();
        obj.transform.GetChild(5).GetChild(1).GetComponentInChildren<TextMesh>().text = i.expToText();
    }
    private void SetPasseport(GameObject obj, Passenger.Passeport p)
    {
        if(!p.present)
        {
            obj.SetActive(false);
            return;
        }

        obj.SetActive(true);
        obj.transform.GetChild(1).GetChild(1).GetComponentInChildren<TextMesh>().text = p.name;
        obj.transform.GetChild(2).GetChild(1).GetComponentInChildren<TextMesh>().text = p.surname;
        obj.transform.GetChild(3).GetChild(1).GetComponentInChildren<TextMesh>().text = p.BirthToText();
        obj.transform.GetChild(5).GetChild(1).GetComponentInChildren<TextMesh>().text = p.DelToText();
        obj.transform.GetChild(6).GetChild(1).GetComponentInChildren<TextMesh>().text = p.ValToText();
    }

    private void SetRuleList(GameObject obj, List<string> rules)
    {
        obj.SetActive(true);
        TextMesh tm = obj.GetComponentInChildren<TextMesh>();
        tm.text = "Pièces obligatoires : \n";
        //always need tickets (level 0 and 1)
        obj.GetComponentInChildren<TextMesh>().text += "- " + rules[0] + "\n";
        //from level 2, add rule for each level
        for (int i = 1; i <= GameManager.currentLevel-1; i++)
        {
            obj.GetComponentInChildren<TextMesh>().text += "- " + rules[i] + "\n";
        }
    }

    private void SetFineMachine(GameObject obj, FineMachineState state)
    {
        switch(state)
        {
            case FineMachineState.IDLE:
                obj.transform.GetChild(4).GetComponentInChildren<TextMesh>().text = "Mettre une amende ?";
                break;
            case FineMachineState.FINE_SELECTED:
                obj.transform.GetChild(4).GetComponentInChildren<TextMesh>().text = "Confirmez ou annuler votre action !";
                break;
        }
    }

}

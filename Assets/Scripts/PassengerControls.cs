using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerControls : MonoBehaviour
{
    Passenger passenger;

    public enum ControlObjectType
    {
        TICKET,
        PASSEPORT,
        RULELIST,
        FINEMACHINE
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
    [SerializeField] private float originalZ = 0;

    RaycastHit2D hit;

    bool fine = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if(hit.collider != null)
            {   
                if(hit.collider.gameObject.CompareTag("FineButton"))
                {
                    Debug.Log("Hit fine button");
                    SetFineMachine(hit.collider.transform.parent.gameObject, FineMachineState.FINE_SELECTED);
                    fine = true;
                }
                
                if(fine && hit.collider.gameObject.CompareTag("ValidateButton"))
                {
                    Debug.Log("FINE VALIDATED");
                    SetFineMachine(hit.collider.transform.parent.gameObject, FineMachineState.IDLE);
                    // Add acion to fine the passenger here
                    fine = false;
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
        passenger = p;
        foreach(ControlObject co in controlObjects)
        {
            switch(co.type)
            {
                case ControlObjectType.TICKET:
                    SetTicket(co.go, p.ticket);
                    break;
                case ControlObjectType.PASSEPORT:
                    SetPasseport(co.go, p.passeport);
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

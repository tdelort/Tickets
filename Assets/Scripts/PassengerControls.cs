using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerControls : MonoBehaviour
{
    // a map from the passenger ticket type to a sprite
    Passenger passenger;

    [SerializeField] GameObject ticketObject;
    [SerializeField] GameObject passeportObject;

    [SerializeField] SpriteRenderer ticketSprite;
    [SerializeField] SpriteRenderer passeportSprite;

    [SerializeField] TextMesh ticketText;
    [SerializeField] TextMesh passeportText;

    public void Set(Passenger p)
    {
        passenger = p;
        SetTicket(p.ticket);
        SetPasseport(p.passeport);
        // etc
    }

    private void SetTicket(Passenger.Ticket t)
    {
        if(!t.present)
        {
            ticketObject.SetActive(false);
            return;
        }

        ticketObject.SetActive(true);
        ticketText.text = t.ToText();
    }

    private void SetPasseport(Passenger.Passeport p)
    {
        if(!p.present)
        {
            passeportObject.SetActive(false);
            return;
        }

        passeportObject.SetActive(true);
        passeportText.text = p.ToText();
    }
}

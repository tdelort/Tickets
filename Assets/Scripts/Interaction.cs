using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{  
    public GameObject usage;
    public GameObject touchE;
    public bool canInteract;
    public bool isInteracting;

    [SerializeField]
    private PassengerControls passengerControls;
    //public GameObject[] usagers = new GameObject[1];
    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
        isInteracting = false;
        //usagers[0] = usage;
    }

    // Update is called once per frame
    void Update()
    {
        //Test Proximité
        //foreach (GameObject usager in usagers)
        //{
        if(Math.Abs(transform.position.x - usage.transform.position.x) <= 1f && !isInteracting){
            touchE.SetActive(true);
            canInteract = true;
            //usage.GetComponent<Image>().SetActive(true);
        }
        if(Math.Abs(transform.position.x - usage.transform.position.x) > 1f){
            touchE.SetActive(false);
            canInteract = false;
            //usage.GetComponent<Image>().SetActive(true);
        }
        //}

        //Clic pour intéragir
        if(canInteract && Input.GetKeyDown(KeyCode.E)){
            // Temporary code : later, we will get the compoenent Passenger from the object and call one of its functions

            Debug.Log("Test started");

            Passenger passenger = gameObject.AddComponent<Passenger>();
            passenger.Init(true, true);

            passengerControls.Set(passenger);
            passengerControls.gameObject.SetActive(true);

            isInteracting = true;
            canInteract = false;
        }    
        else if(isInteracting && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))){
            passengerControls.gameObject.SetActive(false);
            isInteracting = false;
            canInteract = true;
        }
        
        
    }
}

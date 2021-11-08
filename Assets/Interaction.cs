using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{  
    public float playerSpeed = 2.0f;
    public GameObject player;
    public GameObject usage;
    public GameObject touchE;
    public GameObject textTestInteraction;//Mettre UI d'intéraction à la place
    public bool canInteract;
    public bool isInteracting;
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
        //Move to test Interaction
        if(Input.GetAxis("Horizontal") > 0){
            player.transform.Translate(new Vector3(playerSpeed*Time.deltaTime, 0f, 0f));
        }
        else if(Input.GetAxis("Horizontal") < 0){
            transform.Translate(new Vector3(- playerSpeed*Time.deltaTime, 0f, 0f));
        }

        //Test Proximité
        //foreach (GameObject usager in usagers)
        //{
        if(Math.Abs(player.transform.position.x - usage.transform.position.x) <= 50 && !isInteracting){
            touchE = usage.transform.Find("TouchE").gameObject;
            touchE.SetActive(true);
            canInteract = true;
            //usage.GetComponent<Image>().SetActive(true);
        }
        if(Math.Abs(player.transform.position.x - usage.transform.position.x) > 50){
            touchE = usage.transform.Find("TouchE").gameObject;
            touchE.SetActive(false);
            canInteract = false;
            //usage.GetComponent<Image>().SetActive(true);
        }
        //}

        //Clic pour intéragir
        if(canInteract && Input.GetKeyDown(KeyCode.E)){
            textTestInteraction.SetActive(true);
            touchE = usage.transform.Find("TouchE").gameObject;
            touchE.SetActive(false);
            isInteracting = true;
            canInteract = false;
        }    
        else if(isInteracting && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))){
            textTestInteraction.SetActive(false);
            touchE = usage.transform.Find("TouchE").gameObject;
            touchE.SetActive(true);
            isInteracting = false;
            canInteract = true;
        }
        
        
    }
}

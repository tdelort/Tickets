using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public GameObject playerRenderer;
    public bool direction; //true if face left
    

    // Update is called once per frame
    void Update()
    { 

        
        if(Input.GetAxis("Horizontal") > 0){
            transform.Translate(new Vector3(playerSpeed*Time.deltaTime, 0f, 0f));
            direction = true;
            playerRenderer.GetComponent<Transform>().rotation = new Quaternion(0,0,0,0);
        }
        else if(Input.GetAxis("Horizontal") < 0){
            transform.Translate(new Vector3(- playerSpeed*Time.deltaTime, 0f, 0f));
            direction = false;
            playerRenderer.GetComponent<Transform>().rotation = new Quaternion(0,180,0,0);
        }
        
    }
}
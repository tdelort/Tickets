using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 2.0f;
    public SpriteRenderer sprite;
    public Animator animator;
    public float leftValue, rightValue;
    public bool direction; //true if face left
    

    // Update is called once per frame
    void Update()
    { 

        
        if(Input.GetAxis("Horizontal") > 0 && transform.position.x < rightValue){
            transform.Translate(new Vector3(playerSpeed*Time.deltaTime, 0f, 0f));
            direction = true;
            sprite.flipX = false;
            animator.SetBool("isMoving", true);
        }
        else if(Input.GetAxis("Horizontal") < 0 && transform.position.x > leftValue){
            transform.Translate(new Vector3(- playerSpeed*Time.deltaTime, 0f, 0f));
            direction = false;
            sprite.flipX = true;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
    }
}
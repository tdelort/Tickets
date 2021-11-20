using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationUsagers : MonoBehaviour
{

    public Passenger passenger;
    private int score;
    //private Button button;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//TODO: Mettre en argument le passager dont les papiers sont en train d'etre check lors du clic sur le bouton
    public void CheckUsagerWhenAmendeClicked(Passenger passenger){
        if(passenger.isInOrder){
            score -= 100;
        }
        else{
            score += 100;
        }
    }

//TODO: Mettre en argument le passager dont les papiers sont en train d'etre check lors du clic sur le bouton
    public void CheckUsagerWhenEnRegleClicked(Passenger passenger){
        if(passenger.isInOrder){
            score += 100;
        }
        else{
            score -= 100;
        }
    }
}

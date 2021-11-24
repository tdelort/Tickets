using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
    }

    public static void CheckUsagerWhenAmendeClicked(Passenger passenger){
        if(passenger.isInOrder && !passenger.doIllegal){
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            Instance.score -= 100;
            Debug.Log("WRONG ! This passenger was in order");
            passenger.dialogue.SetDialogue(Sentence.SentenceType.BADFINE);
        }
        else{
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            Instance.score += 100;
            Debug.Log("Nice job ! This passenger was not in order or was doing illegal stuff");
            passenger.dialogue.SetDialogue(Sentence.SentenceType.GOODFINE);
        }
    }

    // Pas sur que ca sera utile
    public static void CheckUsagerWhenEnRegleClicked(Passenger passenger){
        if(passenger.isInOrder){
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            Instance.score += 100;
        }
        else{
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            Instance.score -= 100;
        }
    }

}

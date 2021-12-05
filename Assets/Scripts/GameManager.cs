using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        score = 0;
    }

    public static int totalLevels = 3;
    public static int score {get; set;}
    public static int currentLevel {get; set;}
    public static DateTime GameTime { get; set; }
    public static int NbPassenger { get; set; }
    public static int NbNotInOrderPassenger { get; set; }
    public static int NbIlegalActionPassenger { get; set; }
    public bool inPause = false;


    public static void startLevel(int level)
    {
        if(level > 3)
        {
            SceneManager.LoadScene("MainMenu");
            //SceneManager.LoadScene("End");
        }
        setCurrentLevel(level);
        SceneManager.LoadScene("TristanLevel");
    }

    public static int getCurrentLevel()
    {
        return currentLevel;
    }

    public static void setCurrentLevel(int level)
    {
        currentLevel = level;

        NbPassenger = 8;
        NbNotInOrderPassenger = 4;
        NbIlegalActionPassenger = 4;

        switch (level)
        {
            case 0:
                // year - month - day - hour - minute - second
                GameTime = new DateTime(1999, 02, 25, 9, 31, 00);
                break;


            case 1:
                GameTime = new DateTime(1999, 05, 22, 10, 43, 00);
                break;


            case 2:
                GameTime = new DateTime(1999, 12, 31, 18, 10, 00);
                break;

            case 3:
                GameTime = new DateTime(1999, 09, 01, 21, 45, 00);
                break;
        }
    }

    public static void CheckUsagerWhenAmendeClicked(Passenger passenger){
        if(passenger.isInOrder && !passenger.doIllegal){
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            score -= 100;
            Debug.Log("WRONG ! This passenger was in order");
            passenger.dialogue.SetDialogue(Sentence.SentenceType.BADFINE);
        }
        else{
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            score += 100;
            Debug.Log("Nice job ! This passenger was not in order or was doing illegal stuff");
            passenger.dialogue.SetDialogue(Sentence.SentenceType.GOODFINE);
        }
    }

    // Pas sur que ca sera utile
    public static void CheckUsagerWhenEnRegleClicked(Passenger passenger){
        if(passenger.isInOrder){
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            score += 100;
        }
        else{
            //On devra ajouter le code de ce que ça fait spécifiquement dans ce cas-là
            score -= 100;
        }
    }

    // ################# UI PART ####################
    public static void NewGame()
    {
        Debug.Log("New Game");
        startLevel(0);
    }

    public static void ContinueGame()
    {
        Debug.Log("ContinueGame");
        //TODO : Load the last level
        int level = 3;
        startLevel(level);
    }

    public static void OptionsMenu()
    {
        Debug.Log("Options menu");
        //SceneManager.LoadScene("Options");
    }

    public static void CreditsMenu()
    {
        Debug.Log("Credits menu");
        //SceneManager.LoadScene("Credits");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ReturnToMainMenu()
    {
        currentLevel = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public static void BetweenLevels()
    {
        SceneManager.LoadScene("BetweenLevels");
    }

}

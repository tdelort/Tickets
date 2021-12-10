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

    // Saved Data
    public static int totalLevels = 3;
    public static int score {get; set;}
    public static int maxLevel {get; set;}
    public static int migrantAlignment {get; set;}
    public static int propartiAlignment {get; set;}
    public static int resistanteAlignment {get; set;}
    // end Saved Data

    public static int currentLevel {get; set;}
    public static DateTime GameTime { get; set; }
    public static int NbPassenger { get; set; }
    public static int NbNotInOrderPassenger { get; set; }
    public static int NbIlegalActionPassenger { get; set; }
    public bool inPause = false;


    public static void startLevel(int level)
    {
        Debug.Log("##### Starting Level " + level + " #####");
        if(level > 3)
        {
            ReturnToMainMenu();
            //SceneManager.LoadScene("End");
        }
        else if(level == 0)
        {
            setCurrentLevel(level);
            SceneManager.LoadScene("Tutorial");
        }
        else
        {
            setCurrentLevel(level);
            SceneManager.LoadScene("JournalScene");
        }
    }
 
    public static int getCurrentLevel()
    {
        return currentLevel;
    }

    public static void setCurrentLevel(int level)
    {
        currentLevel = level;
        maxLevel = currentLevel > maxLevel ? currentLevel : maxLevel;

        //must be NbPassenger >= 3
        NbPassenger = 12;
        NbNotInOrderPassenger = 4;
        NbIlegalActionPassenger = 0;

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
        ResetGame();
        SaveGame(); //To erase the old save
        Debug.Log("New Game");
        //TODO remettre à zero
        startLevel(0);
    }

    public static void ContinueGame()
    {
        Debug.Log("ContinueGame");
        LoadGame();
        startLevel(maxLevel);

    }

    public static void OptionsMenu()
    {
        Debug.Log("Options menu");
        //SceneManager.LoadScene("Options");
    }

    public static void CreditsMenu()
    {
        Debug.Log("Credits menu");
        SceneManager.LoadScene("Credits");
    }

    public static void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }

    public static void ReturnToMainMenu()
    {
        SaveGame();
        currentLevel = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public static void BetweenLevels()
    {
        // The player as finished the level "currentLevel" hence the +1
        maxLevel = currentLevel + 1 > maxLevel ? currentLevel + 1 : maxLevel;
        SceneManager.LoadScene("BetweenLevels");
    }

    public static void AfterJournal()
    {
        SceneManager.LoadScene("TristanLevel");
    }

    private static void ResetGame()
    {
        score = 0;
        maxLevel = 0;
        migrantAlignment = 0;
        propartiAlignment = 0;
        resistanteAlignment = 0;
    }

    private static void SaveGame()
    {
        SaveData data = new SaveData(
            maxLevel,
            score,
            migrantAlignment,
            propartiAlignment,
            resistanteAlignment
        );
        SaveSystem.Write(data);
    }

    private static void LoadGame()
    {
        SaveData data = SaveSystem.Read();
        if (data != null)
        {
            maxLevel = data.level;
            score = data.score;
            migrantAlignment = data.migrantAlignment;
            propartiAlignment = data.propartiAlignment;
            resistanteAlignment = data.resistanteAlignment;
        }
        else
        {
            maxLevel = 0;
            score = 0;
            migrantAlignment = 0;
            propartiAlignment = 0;
            resistanteAlignment = 0;
        }
    }
}

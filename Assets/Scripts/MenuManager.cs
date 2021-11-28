using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool inPause = false;
    public GameObject pauseMenu;
    public void SelectLevelMenu(){
        SceneManager.LoadScene("LevelSelection");
    }

    public void NewGame(){
        //TODO:Implement New game
        //Lancer le tuto 0
    }

    public void ContinueGame(){
        //TODO:Implement Continue
        //Lancer le niveau sauvegardé
    }

    public void OptionsMenu(){
        //SceneManager.LoadScene("Options");
    }

    public void CreditsMenu(){
        //SceneManager.LoadScene("Credits");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void ReturnToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame(){
        inPause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update(){
        /*
        if(Input.GetKeyDown(KeyCode.Space) && !inPause){
            inPause = true;
            Debug.Log("Oui");
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        if(inPause == true){
            
        }
        if(inPause == false){
            Time.timeScale = 1f;
        }
        */
        /*
        if(Input.GetKeyDown(KeyCode.Space))
            inPause = !inPause;
        
        if(inPause)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
            */
        
    }
}

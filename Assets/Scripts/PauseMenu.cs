using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitButton;
    bool inPause = false;

    void Start()
    {
        resumeButton.onClick.AddListener(() => { PauseGame(); });
        quitButton.onClick.AddListener(() => { PauseGame(); GameManager.ReturnToMainMenu(); });
        Debug.Assert(pauseMenu != null, "PauseMenu is null");
    }

    public void PauseGame()
    {
        if(!inPause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            inPause = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            inPause = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause Menu"))
            PauseGame();
    }
}

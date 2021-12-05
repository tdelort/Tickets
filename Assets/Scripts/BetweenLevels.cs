using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetweenLevels : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text personalityText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        scoreText.text = "Score: " + GameManager.score.ToString();
        string personality = "ALIGNEMENT";
        personalityText.text = "Vous êtes en majorité du côté des " + personality;
        nextLevelButton.onClick.AddListener(() => {
            GameManager.startLevel(GameManager.currentLevel + 1);
        });
        mainMenuButton.onClick.AddListener(() => {
            GameManager.ReturnToMainMenu();
        });

        if(GameManager.currentLevel >= GameManager.totalLevels)
        {
            nextLevelButton.gameObject.SetActive(false);
        }

    }
}

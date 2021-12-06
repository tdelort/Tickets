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

        personalityText.text = "";
        if(GameManager.currentLevel >= 1)
        {
            personalityText.text = "Alignement :\n";
            personalityText.text += "* Proparti : " + (GameManager.propartiAlignment > 0 ? "Aidé" : (GameManager.propartiAlignment < 0 ? "Bloqué" : "Ignoré")) + "\n";
        }
        if(GameManager.currentLevel >= 2)
        {
            personalityText.text += "* Migrant : " + (GameManager.migrantAlignment > 0 ? "Aidé" : (GameManager.migrantAlignment < 0 ? "Bloqué" : "Ignoré")) + "\n";
        }
        if(GameManager.currentLevel >= 3)
        {
            personalityText.text += "* Resistante : " + (GameManager.resistanteAlignment > 0 ? "Aidé" : (GameManager.resistanteAlignment < 0 ? "Bloqué" : "Ignoré")) + "\n";
        }

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

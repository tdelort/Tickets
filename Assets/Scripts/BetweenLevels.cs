using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetweenLevels : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text scoreNeedText;
    [SerializeField] private Text personalityText;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Image endbg;

    public int NbFineNeedForFinishLevel = 2;

    private int score;
    private int scorePassage;

    void Start()
    {
        score = GameManager.score;
        //tuto + level
        scorePassage = 100 + GameManager.currentLevel * NbFineNeedForFinishLevel * 100;

        scoreText.text = score.ToString();

        scoreNeedText.text = scorePassage.ToString();

        personalityText.text = "Les faits marquants de votre aventure :\n";
        if (GameManager.currentLevel >= 1)
        {
            
            personalityText.text += "- Usager proparti fraudeur : " + (GameManager.propartiAlignment > 0 ? "Aidé" : (GameManager.propartiAlignment < 0 ? "Arrêté" : "Ignoré")) + "\n";
        }
        if(GameManager.currentLevel >= 2)
        {
            personalityText.text += "- Usager Migrant fraudeur: " + (GameManager.migrantAlignment > 0 ? "Aidé" : (GameManager.migrantAlignment < 0 ? "Arrêté" : "Ignoré")) + "\n";
        }
        if(GameManager.currentLevel >= 3)
        {
            personalityText.text += "- Usager Resistante fraudeuse: " + (GameManager.resistanteAlignment > 0 ? "Aidé" : (GameManager.resistanteAlignment < 0 ? "Arrêté" : "Ignoré")) + "\n";
        }
        if (score < scorePassage)
        {
            endbg.color = new Color(147f/255f, 62f / 255f, 60f / 255f, 255f / 255f);
            nextLevelButton.transform.GetChild(0).GetComponent<Text>().text = "Recommencer";
            nextLevelButton.onClick.AddListener(() => {
                GameManager.startLevel(GameManager.currentLevel);
            });
        }
        else
        {
            endbg.color = new Color(89f / 255f, 159f / 255f, 86f / 255f, 255f / 255f);
            nextLevelButton.onClick.AddListener(() => {
                GameManager.startLevel(GameManager.currentLevel + 1);
            });
            
        }
        
        mainMenuButton.onClick.AddListener(() => {
            GameManager.ReturnToMainMenu();
        });

        if(GameManager.currentLevel >= GameManager.totalLevels)
        {
            nextLevelButton.gameObject.SetActive(false);
        }

    }
}

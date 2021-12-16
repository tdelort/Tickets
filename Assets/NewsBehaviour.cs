using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsBehaviour : MonoBehaviour
{
    public GameObject headLine;
    public Text text1, text2, text3;
    // Start is called before the first frame update
    public int uichoice = 1;
    void Start()
    {
        int choice = GameManager.currentLevel;
        choice = uichoice;
        if (choice == 1)
        {
            headLine.GetComponent<TextMesh>().text = "ELECTIONS PRESIDENTIELLES";
            text1.text = "Le parti du travail accède au pouvoir avec 60% des voix. Nous continuerons de vous informer des premieres mesures de ce nouveau parti et espérons prospérité pour notre beau pays. rendez vous en page 2 pour en savoir plus sur le parti";
            text2.text = "Victoire de l'équipe nationale d'eudéo dans son match de préparation au mondial de football. Retour avec les commentaires du coach sur le match en page 3.";
            text3.text = "Les sanctions pour titre de transport manquant sont revues à la hausse. Préparez vous a des amende allant jusqu'a 10 000£ et 6 mois emprisonnement en cas de récidive.";
        }
        if (choice == 2)
        {
            headLine.GetComponent<TextMesh>().text = "RENATIONALISATION DU PAYS";
            text1.text = "Une des premieres mesures avec celles d'hier du nouveau parti a été de lancer une campagne d'expulsion des sans-papiers de notre pays. Une action a déjà été lancé dans le quartier est et une petite centaine de personne y ont déjà été expulsé.";
            text2.text = "Les matchs de Football sont suspendus jusqu'à nouvel ordre. Retour avec l'ensemble de l'équipe sur l'année parcouru en page 2 et organisation de notre propre compétition au sein de notre pays.";
            text3.text = "Il y a maintenant de nouvelles règles de déplacements, tous citoyens peut se déplacer librement à condition qu'il ait avec soit un passeport ou une pièce d'identité valide. Il y a également de nouveaux controlleurs dans les rames de métro ou dans les centres commerciaux";
        }
        if (choice == 3)
        {
            headLine.GetComponent<TextMesh>().text = "L' ANCIEN PARTI S'ORGANISE";
            text1.text = "Il a été découvert que l'ancien parti s'organise en groupe afin de mener des actions terroristes contre le parti en place. Il est demandé de reporter à la police toutes personnes ou activités suspectes afin de prévenir tous problèmes.";
            text2.text = "Les règles dans les transports risquent de rester jusqu'à nouvel ordre et un renforcement des contrôles sera effectués dans les trains. Il y a interdiction d'entrer dans le pays jusqu'à nouvel ordre";
            text3.text = "La loi sur l'immigration a permit de nettoyer d'autres quartiers et d'arretter d'autres personnes illégales. Le porte parole du gouvernement prendra l'antenne à 19h ce soir pour réagir sur cette victoire.";
        }
        
    }

    public void SceneChange()
    {
        GameManager.AfterJournal();
    }
    
}

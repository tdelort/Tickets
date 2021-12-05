using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsBehaviour : MonoBehaviour
{
    public GameObject headLine;
    public GameObject text1, text2, text3;
    // Start is called before the first frame update
    void Start()
    {
        int choice = GameManager.currentLevel;
        if (choice == 1)
        {
            headLine.GetComponent<TextMesh>().text = "ELECTIONS PRESIDENTIELLES";
            text1.GetComponent<TextMesh>().text = "Le parti du travail accède \nau pouvoir avec 60% des \nvoix. Nous continuerons\nde vous informer des\n premieres mesures de ce \nnouveau parti et espérons \nprospérité pour notre beau \npays. rendez vous \nen page 2 pour en savoir \nplus sur le parti";
            text2.GetComponent<TextMesh>().text = "Victoire de l'équipe nationale d'eudéo dans \nson match de préparation au mondial de \nfootball. Retour avec les commentaires du \ncoach sur le match en page 3.";
            text3.GetComponent<TextMesh>().text = "Il y a maintenant de nouvelles règles de \ndéplacements, tous citoyens peut se \ndéplacer librement à condition qu'il ait \navec soit un passeport valide. Il y a également\nde nouveaux controlleurs dans les rames \nde métro ou dans les centres commercials";
        }
        if (choice == 2)
        {
            headLine.GetComponent<TextMesh>().text = "RENATIONALISATION DU PAYS";
            text1.GetComponent<TextMesh>().text = "Une des premieres mesures \navec celles d'hier du nouveau \nparti a été de lancer une \ncampagne d'expulsion des \nsans-papiers de notre pays. Une \naction a déjà été lancé dans le \nquartier est et une petite \ncentaine de personne y ont déjà \nété expulsé.";
            text2.GetComponent<TextMesh>().text = "Les citoyens de l'Eudéo ont maintenant besoin \nd'une autorisation de sortie afin de pouvoir \ncontroler d'une meilleur façon les sans papiers \net de pouvoir mener à bien l'opération.";
            text3.GetComponent<TextMesh>().text = "Les matchs de Football sont suspendus jusqu'à \nnouvel ordre. Retour avec l'ensemble de \nl'équipe sur l'année parcouru en page 2 \net organisation de notre propre compétition \nau sein de notre pays.";
        }
        if (choice == 3)
        {
            headLine.GetComponent<TextMesh>().text = "L' ANCIEN PARTI S'ORGANISE";
            text1.GetComponent<TextMesh>().text = "Il a été découvert que l'ancien \nparti s'organise en groupe afin \nde mener des actions \nterroristes contre le parti en \nplace. Il est demandé de \nreporter à la police toutes \npersonnes ou activités \nsuspectes afin de prévenir \ntous problèmes.";
            text2.GetComponent<TextMesh>().text = "Je ne me rappelle plus de la nouvelle règle et ce n'est pas écrit dans le GDD. écrire un texte a mettre ici";
            text3.GetComponent<TextMesh>().text = "Les actions d'hier et d'avant-hier a pu \npermettre de nettoyer d'autres quartiers \net d'arretter d'autres personnes illégales. ";
        }
        
    }

    public void SceneChange()
    {
        GameManager.AfterJournal();
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private Text endText;

    private void Start()
    {
        endText.text = "END ERROR";
        //si vous avez laiss� passer le pro parti
        if (GameManager.propartiAlignment >= 0)
        {
            //si vous avez laiss� passer le migrant
            if (GameManager.migrantAlignment >= 0)
            {
                //si vous avez accept� la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous avez fui le Pays sans encombre." +
                        "Vous avez perdu votre maison et la majorit� de vos possessions," +
                        "mais vous �tes en vie.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text = "Vous devenez un passeur pour les gens d�munis. " +
                        "Vous risquez votre vie jour apr�s jour pour celle des autres.";
                //si vous avez arr�t� la resistante
                else
                    endText.text = "Vous ne fa�tes pas de vagues et aidez les plus d�muni." +
                        "Elle a vu plusieurs fois et au d�tour d'un contr�le, vous � propos� un verre." +
                        "Puis un d�ner. Puis une alliance. Vive les mari�s.";
            }
            else
            {
                //si vous avez accept� la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous avez tent� de fuir. Mais comme beaucoup, vous vous �tes fait arr�ter par " +
                         "des gardes en traversant un no man's land. Jet� dans une prison d'�tat, vous vous retrouv� " +
                         "� creuser des trous � longueur de journ�e.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text = "Vous vous servez de vos connections avec le pouvoir pour r�cup�rer des informations..." +
                        " que vous vous empressez de communiquer aux r�sistants. Vous �tes devenu une taupe, efficace et " +
                        "sans scrupule. Ce r�gime ne fera pas long feu gr�ce � vous.";
                //si vous avez arr�t� la resistante
                else
                    endText.text =
                        "Vous �tes le fleuron du pays. La cr�me de la cr�me. " +
                        "Gr�ce � vous la nation a pu se reprendre en main. " +
                        "Vous avez �t� m�daill� par un haut dirigeant pour vos service sans reproche.";
            }
        }
        else
        {
            //si vous avez laiss� passer le migrant
            if (GameManager.migrantAlignment >= 0)
            {
                //si vous avez accept� la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous tentez de fuir le pays mais ne r�ussissez pas � franchir la fronti�re car" +
                        " vous avez �t� surveill�. Mais vous avez r�ussi � vous �chapper avec l'aide de vos compagnons de " +
                        "gal�re. Impossible de reprendre une vie normale, vous devenez un rebelle notoire.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text =
                        "Vous embrassez totalement la vie de rebelle. Vous ne tardez pas � monter les �chelons et " +
                        "� devenir l'un des principaux leader de la r�sistance.";
                //si vous avez arr�t� la resistante
                else
                    endText.text = "Vous pensiez �tre � l'abri du parti.. Mais il a d�couvert que vous �tiez un peu trop gentil " +
                        "avec les migrants. Vous avez �t� d�nonc� et finissez dans les cellules d'une prison d'�tat.";
            }
            else
            {
                //si vous avez accept� la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous avez essay� de fuir. Il a suffi d'un coup de feu pour que votre groupe" +
                        " de migrant s'enfuit en vous laissant l�. De ce m�me coup de feu qui vous a enlev� la vie." +
                        "Vous �tes mort.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text = "Vous n'aimez pas la tournure des �v�nements. Vous �tes pr�t � beaucoup " +
                        "de sacrifices pour que la r�sistance puisse reprendre le pouvoir. Aux informations, " +
                        "tant�t on vous appel rebelle, tant�t terroriste.";
                //si vous avez arr�t� la resistante
                else
                    endText.text = "Vous fa�te bien votre travail. Vous ne vous m�lez pas des affaires qui" +
                        " ne vous regarde pas. Vous avez donc �t� nomm� chef de la s�curit� de votre bo�te. " +
                        "Une retraite paisible vous attends s�rement au bout de vos longues ann�es de travail.";
            }
        }
    }

    void Update()
    {
        //Fade
        if (transform.position.x < 10)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, transform.position.x / 10);
        }
        //End
        if (transform.position.x > 0)
            transform.Translate(Vector2.left * Time.deltaTime * 1f);

    }

    public void Stop()
    {
        GameManager.CreditsMenu();
    }
}

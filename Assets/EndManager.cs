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
        //si vous avez laissé passer le pro parti
        if (GameManager.propartiAlignment >= 0)
        {
            //si vous avez laissé passer le migrant
            if (GameManager.migrantAlignment >= 0)
            {
                //si vous avez accepté la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous avez fui le Pays sans encombre." +
                        "Vous avez perdu votre maison et la majorité de vos possessions," +
                        "mais vous êtes en vie.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text = "Vous devenez un passeur pour les gens démunis. " +
                        "Vous risquez votre vie jour après jour pour celle des autres.";
                //si vous avez arrêté la resistante
                else
                    endText.text = "Vous ne faîtes pas de vagues et aidez les plus démuni." +
                        "Elle a vu plusieurs fois et au détour d'un contrôle, vous à proposé un verre." +
                        "Puis un dîner. Puis une alliance. Vive les mariés.";
            }
            else
            {
                //si vous avez accepté la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous avez tenté de fuir. Mais comme beaucoup, vous vous êtes fait arrêter par " +
                         "des gardes en traversant un no man's land. Jeté dans une prison d'état, vous vous retrouvé " +
                         "à creuser des trous à longueur de journée.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text = "Vous vous servez de vos connections avec le pouvoir pour récupérer des informations..." +
                        " que vous vous empressez de communiquer aux résistants. Vous êtes devenu une taupe, efficace et " +
                        "sans scrupule. Ce régime ne fera pas long feu grâce à vous.";
                //si vous avez arrêté la resistante
                else
                    endText.text =
                        "Vous êtes le fleuron du pays. La crème de la crème. " +
                        "Grâce à vous la nation a pu se reprendre en main. " +
                        "Vous avez été médaillé par un haut dirigeant pour vos service sans reproche.";
            }
        }
        else
        {
            //si vous avez laissé passer le migrant
            if (GameManager.migrantAlignment >= 0)
            {
                //si vous avez accepté la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous tentez de fuir le pays mais ne réussissez pas à franchir la frontière car" +
                        " vous avez été surveillé. Mais vous avez réussi à vous échapper avec l'aide de vos compagnons de " +
                        "galère. Impossible de reprendre une vie normale, vous devenez un rebelle notoire.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text =
                        "Vous embrassez totalement la vie de rebelle. Vous ne tardez pas à monter les échelons et " +
                        "à devenir l'un des principaux leader de la résistance.";
                //si vous avez arrêté la resistante
                else
                    endText.text = "Vous pensiez être à l'abri du parti.. Mais il a découvert que vous étiez un peu trop gentil " +
                        "avec les migrants. Vous avez été dénoncé et finissez dans les cellules d'une prison d'état.";
            }
            else
            {
                //si vous avez accepté la proposition de la resistante
                if (GameManager.resistanteAlignment > 0)
                    endText.text = "Vous avez essayé de fuir. Il a suffi d'un coup de feu pour que votre groupe" +
                        " de migrant s'enfuit en vous laissant là. De ce même coup de feu qui vous a enlevé la vie." +
                        "Vous êtes mort.";
                //si vous avez rejoins la resistante
                else if (GameManager.resistanteAlignment == 0)
                    endText.text = "Vous n'aimez pas la tournure des évènements. Vous êtes prêt à beaucoup " +
                        "de sacrifices pour que la résistance puisse reprendre le pouvoir. Aux informations, " +
                        "tantôt on vous appel rebelle, tantôt terroriste.";
                //si vous avez arrêté la resistante
                else
                    endText.text = "Vous faîte bien votre travail. Vous ne vous mêlez pas des affaires qui" +
                        " ne vous regarde pas. Vous avez donc été nommé chef de la sécurité de votre boîte. " +
                        "Une retraite paisible vous attends sûrement au bout de vos longues années de travail.";
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Image fade;
    void Update()
    {
        //Fade
        if(transform.position.x < -60)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, (transform.position.x + 60) / -6);
        }
        //End
        if(transform.position.x < -66)
            Stop();

        transform.Translate(Vector2.left * Time.deltaTime * 1f);
    }

    public void Stop()
    {
        GameManager.ReturnToMainMenu();
    }
}

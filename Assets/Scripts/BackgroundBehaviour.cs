using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// il faut update les position de base et exit camera afin d'adapter a la scene

public class BackgroundBehaviour : MonoBehaviour
{
    private Vector3 position = new Vector3(0,0,0);
    private float speed; //pour avoir la vitesse
    private bool leaving = false; //pour savoir si le métro pars de l'arret
    private bool arriving = false; //pour savoir si le métro arrive à l'arret
    private Vector3 baseposition = new Vector3(-19,1.3f,0);
    public GameObject[] Doors;
    

    void Start()
    {
        Arrive();
    }
    // Update is called once per frame
    void Update()
    {
        
        if (leaving == true) //si le métro pars, cette partie de code change sa position
        {
            position.Set(this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);
            this.transform.position = position;
            if (this.transform.position.x >  - baseposition[0])
            {
                leaving = false;
                this.transform.position = baseposition;
            }
        }


        if (arriving == true) //si le métro arrive, cette partir de code change sa position
        {   
            position.Set(this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);
            this.transform.position = position;
            if (this.transform.position.x > 0)
            {
                foreach(GameObject o in Doors)
                {
                    o.GetComponent<DoorBehaviour>().DoorOpen(); //pour faire ouvrir les portes dès que le métro pars
                }
                arriving = false;
            }
            
        }

    }


    IEnumerator SpeedModifier(float multiply) //coroutine pour accelérer le métro
    {
        while(arriving == true || leaving == true)
        {
            speed = speed * multiply;
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void Leave() //appelé pour faire partir le métro
    {
        foreach(GameObject o in Doors)
        {
            o.GetComponent<DoorBehaviour>().DoorClose(); //pour faire fermer les portes dès que le métro pars
        }
        speed = 0.01f;
        leaving = true;
        StartCoroutine(SpeedModifier(1.2f));
    }

    public void Arrive() //appelé pour faire arriver le métro
    {
        speed = 0.2f;
        arriving = true;
        StartCoroutine(SpeedModifier(0.8f));
    }
}

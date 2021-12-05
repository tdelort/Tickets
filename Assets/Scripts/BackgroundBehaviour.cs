using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// il faut update les position de base et exit camera afin d'adapter a la scene

public class BackgroundBehaviour : MonoBehaviour
{
    public UnityEvent OnFullStop = new UnityEvent();

    private float speed = 20f; //pour avoir la vitesse
    private bool leaving = false; //pour savoir si le métro pars de l'arret
    private bool arriving = false; //pour savoir si le métro arrive à l'arret
    private Vector3 baseposition;
    private Vector3 targetposition;
    private Vector3 exitposition;
    public GameObject[] Doors;
    
    void Start()
    {
        baseposition = transform.position;
        targetposition = transform.position;
        exitposition = transform.position;
        baseposition.x = -20;
        exitposition.x = 20;
    }

    public void Leave() //appelé pour faire partir le métro
    {
        StartCoroutine(LeaveCoroutine());
    }

    public void Arrive() //appelé pour faire arriver le métro
    {
        StartCoroutine(ArriveCoroutine());
    }

    IEnumerator ArriveCoroutine()
    {
        bool condition = false;
        transform.position = baseposition;
        do
        {
            transform.position  = Vector3.MoveTowards(transform.position, targetposition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            condition = (transform.position == targetposition);
        } while (!condition);

        //Open doors
        foreach(GameObject o in Doors)
        {
            o.GetComponent<DoorBehaviour>().DoorOpen();
        }

        yield return new WaitForSeconds(0.5f);
        Debug.LogWarning("INVOKE");
        OnFullStop.Invoke();
    }

    IEnumerator LeaveCoroutine()
    {
        transform.position = targetposition;
        foreach(GameObject o in Doors)
        {
            o.GetComponent<DoorBehaviour>().DoorClose();
        }
        yield return new WaitForSeconds(1);
        bool condition = false;
        do
        {
            transform.position = Vector3.MoveTowards(transform.position, exitposition, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            condition = (transform.position == exitposition);
        } while (!condition);
    }
}

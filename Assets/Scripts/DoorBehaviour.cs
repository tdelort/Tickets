using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    public GameObject door1, door2; //door1 = porte droite
    private Vector3 baseposition1, baseposition2;
    private bool opendoor,closedoor;
    private Vector3 diffVector = new Vector3(1,0,0);

    // Start is called before the first frame update
    void Start()
    {
        baseposition1 = door1.transform.position;
        baseposition2 = door2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (opendoor == true)
        {
            door1.transform.position = Vector3.MoveTowards(door1.transform.position, baseposition1 + diffVector, 0.01f);
            door2.transform.position = Vector3.MoveTowards(door2.transform.position, baseposition2 - diffVector, 0.01f);
            if(door1.transform.position == baseposition1 + diffVector)
            {
                opendoor = false;
            }
        }
        if (closedoor == true)
        {
            door1.transform.position = Vector3.MoveTowards(door1.transform.position, baseposition1, 0.01f);
            door2.transform.position = Vector3.MoveTowards(door2.transform.position, baseposition2, 0.01f);
            if(door1.transform.position == baseposition1)
            {
                closedoor = false;
            }
        }
    }
    public void DoorOpen()
    {
        opendoor = true;
    }

    public void DoorClose()
    {
        closedoor = true;
    }
}

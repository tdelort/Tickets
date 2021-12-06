using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerControlsTests : MonoBehaviour
{

    [SerializeField]
    private PassengerControls passengerControls;

    // Update is called once per frame
    void Update()
    {
        // On space start tests
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            //Debug.Log("Test started");

            Passenger passenger = gameObject.AddComponent<Passenger>();
            passenger.Init(true, true);

            passengerControls.Set(passenger);
            passengerControls.gameObject.SetActive(true);
        }
    }
}

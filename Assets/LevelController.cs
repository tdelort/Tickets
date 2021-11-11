using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLevelController : MonoBehaviour
{
    public GameObject Spawnpoints;
    public GameObject passengerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
        spawnPassengers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnPassengers()
    {
        int nbNIOPass = GameData.NbNotInOrderPassenger;
        int nbIlePass = GameData.NbIlegalActionPassenger;
        for (int i = 0; i < GameData.NbPassenger; i++){
            GameObject passenger = Instantiate(passengerPrefab,
                Spawnpoints.transform.GetChild(i).position, Quaternion.identity);
            if (nbNIOPass > 0)
            {
                passenger.GetComponent<Passenger>().Init(false, true);
                nbNIOPass--;
            }
            else if (nbIlePass > 0)
            {
                passenger.GetComponent<Passenger>().Init(true, false);
                nbIlePass--;
            }
            else
            {
                passenger.GetComponent<Passenger>().Init(true, true);
            }

                passenger.SetActive(true);
            passenger.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < Spawnpoints.transform.childCount; i++)
        {
            int rnd = Random.Range(0, Spawnpoints.transform.childCount);
            Spawnpoints.transform.GetChild(i).SetSiblingIndex(rnd);
        }
    }
}

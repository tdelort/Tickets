using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject SpawnpointsSource;
    public GameObject passengerPrefab;

    private GameObject[] Spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        spawnPassengers();
        for (int i = 0; i < SpawnpointsSource.transform.childCount; i++)
        {
            Spawnpoints[i] = SpawnpointsSource.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnPassengers()
    {
        for (int i = 0; i < GameData.NbPassenger; i++){
            GameObject passenger = Instantiate(passengerPrefab, new Vector3(0, 0, 0.1f), Quaternion.identity);
        }
    }
}

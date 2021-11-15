using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    private Vector3 spawnPoint;
    private float min, max;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public void SetBounds(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

}

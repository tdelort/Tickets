using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionDisparition : MonoBehaviour
{
    public GameObject prefabUsager;
    private int numberOfUsager;
    private GameObject[] usagers;
    private int[] positionXtoGo;
    private int x;
    private float desiredDuration = 3f;
    private float[] elapsedTime;
    private float[] percentageCompleted;
    private Vector3[] endPosition;
    private Vector3 startPosition;
    private float percentageCompletedToRemove;
    private float elapsedTimeToRemove;

    // Start is called before the first frame update
    void Start()
    {   
        numberOfUsager = Random.Range(2,5);
        x = Random.Range(-5,5);
        startPosition = new Vector3(x, 0, 0);
        usagers = new GameObject[numberOfUsager];
        positionXtoGo = new int[numberOfUsager];
        percentageCompleted = new float[numberOfUsager];
        endPosition = new Vector3[numberOfUsager];
        elapsedTime = new float[numberOfUsager];
        for (int i = 0; i < numberOfUsager; i++){
            positionXtoGo[i] = Random.Range(-5,5);
            endPosition[i] = new Vector3(positionXtoGo[i], 0, 0);
            usagers[i] = Instantiate(prefabUsager, new Vector3(x, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < numberOfUsager; i++){
            elapsedTime[i] += Time.deltaTime;
            percentageCompleted[i] = elapsedTime[i] / desiredDuration;
            //usagers[i].transform.position = transform.position;
            usagers[i].transform.position = Vector3.Lerp(startPosition, endPosition[i], percentageCompleted[i]);
        }
        Debug.Log(percentageCompleted[0]);

        //Test to delete one of the usagers
        /*
        if(Input.GetKeyDown(KeyCode.Space)){
            DisappearUsager(usagers[0]);
        }
        */
    }

    void DisappearUsager(GameObject usager){
        //float elapsedTimeToRemove;
        //float percentageCompletedToRemove;
        elapsedTimeToRemove += Time.deltaTime;
        percentageCompletedToRemove = elapsedTimeToRemove / desiredDuration;
        usager.transform.position  = Vector3.Lerp(usager.transform.position, new Vector3(x, 0, 0), percentageCompletedToRemove);
        if (percentageCompletedToRemove >= 1){
            Destroy(usager);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLevelController : MonoBehaviour
{
    public bool isMoving = false;
    private float internalClock = 0;
    public int numberOfStop;
    public int numberOfPassenger;
    private int actualNumberOfPassenger = 0;
    public GameObject passengerPrefab;
    

    // Metro doors position
    public Transform[] spawnPoints;

    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private List<GameObject> passengers = new List<GameObject>();
    private List<Vector2> startPositions = new List<Vector2>();
    private List<Vector2> endPositions = new List<Vector2>();

    

    float elapsedTime = 0;
    float percentageCompleted = 0;
    float desiredDuration = 3f;


    // Start is called before the first frame update
    private void Start() {
        numberOfPassenger = GameData.NbPassenger;
        numberOfPassengerComingIn = (int)Random.Range(3,numberOfPassenger - actualNumberOfPassenger);
        Debug.Log(numberOfPassengerComingIn + " passenger come in the train");

    }
    private void createPassenger(int nbNIOPass, int nbIlePass)
    {
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        GameObject passenger = Instantiate(passengerPrefab, spawnPoint, Quaternion.identity);
        passengers.Add(passenger);
        Vector2 endPosition = new Vector2(Random.Range(leftBound.position.x, rightBound.position.x), spawnPoint.y);
        passenger.GetComponent<Passenger>().position(spawnPoint, endPosition);
        if (nbNIOPass > 0)
        {
            passenger.GetComponent<Passenger>().Init(false, false);
            nbNIOPass--;
        }
        else if (nbIlePass > 0)
        {
            passenger.GetComponent<Passenger>().Init(true, true);
            nbIlePass--;
        }
        else
        {
            passenger.GetComponent<Passenger>().Init(true, false);
        }

        passenger.SetActive(true);
        passenger.transform.GetChild(0).gameObject.SetActive(false);
    }
    private int numberOfPassengerComingIn;
    private int numberOfPassengerLeaving;
    void Update()
    {
        
        //to manage if train is running or not
        internalClock += Time.deltaTime;
        if (internalClock > 6 && isMoving == false)
        {
            isMoving = true;
            internalClock = 0;
        }
        if (internalClock > 15 && isMoving == true)
        {
            numberOfPassengerLeaving = (int)Random.Range(0,actualNumberOfPassenger);
            Debug.Log(numberOfPassengerLeaving + " passenger have left the train");
            actualNumberOfPassenger -= numberOfPassengerLeaving;
            numberOfPassengerComingIn = (int)Random.Range(0,numberOfPassenger - actualNumberOfPassenger);
            Debug.Log(numberOfPassengerComingIn + " passenger come in the train");
            isMoving = false;
            internalClock = 0;

            FindObjectOfType<Interaction>().EndInteract();
        }
        //create passager while the train is stopped
        int nbNIOPass = GameData.NbNotInOrderPassenger;
        int nbIlePass = GameData.NbIlegalActionPassenger;
        
        for(int i = 0;i < numberOfPassengerLeaving; i++)
        {
            passengers[0].GetComponent<Passenger>().leave();
            passengers.RemoveAt(0);
        }
        numberOfPassengerLeaving = 0;

        for(int i = 0;i < numberOfPassengerComingIn; i++)
        {
            createPassenger(nbNIOPass, nbIlePass);
            actualNumberOfPassenger += 1;
        }
        numberOfPassengerComingIn = 0;

        

    }
}

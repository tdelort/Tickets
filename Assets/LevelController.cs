using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public bool isMoving = false;
    private float internalClock = 0;
    public int numberOfPassenger;
    private int actualNumberOfPassenger = 0;
    public GameObject passengerPrefab;
    
    [SerializeField] private Interaction interaction;
    // Metro doors position
    public Transform[] spawnPoints;

    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private List<GameObject> passengers = new List<GameObject>();

    [SerializeField] GameObject propartiPrefab;
    [SerializeField] GameObject migrantPrefab;
    [SerializeField] GameObject resistantePrefab;
    
    [SerializeField] private BackgroundBehaviour bb;

    // Start is called before the first frame update
    private void Start() {
        numberOfPassenger = GameManager.NbPassenger;
        numberOfPassengerComingIn = (int)Random.Range(3,numberOfPassenger - actualNumberOfPassenger);
        Debug.Log(numberOfPassengerComingIn + " passenger come in the train");

        // For now the special passengers will spawn in the start
        SpecialPassenger sp;
        switch (GameManager.getCurrentLevel())
        {
            case 1:
                sp = Instantiate(propartiPrefab, new Vector2(0, 0), Quaternion.identity).GetComponent<SpecialPassenger>();
                sp.Init(SpecialPassengerType.PROPARTI);
                break;
            case 2:
                sp = Instantiate(migrantPrefab, new Vector2(0, 0), Quaternion.identity).GetComponent<SpecialPassenger>();
                sp.Init(SpecialPassengerType.MIGRANT);
                break;
            case 3:
                sp = Instantiate(resistantePrefab, new Vector2(0, 0), Quaternion.identity).GetComponent<SpecialPassenger>();
                sp.Init(SpecialPassengerType.RESISTANTE);
                break;
            default:
                Debug.Log("No special passenger for this level YET");
                break;
        }

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
            //Debug.Log("NOT IN ORDER");
            passenger.GetComponent<Passenger>().Init(false, false);
            nbNIOPass--;
        }
        else if (nbIlePass > 0)
        {
            //Debug.Log("ILEGAL");
            passenger.GetComponent<Passenger>().Init(true, true);
            nbIlePass--;
        }
        else
        {
            //Debug.Log("ORDER");
            passenger.GetComponent<Passenger>().Init(true, false);
        }

        passenger.SetActive(true);
        passenger.transform.GetChild(0).gameObject.SetActive(false);
    }
    private int numberOfPassengerComingIn;
    private int numberOfPassengerLeaving;

    int nbStops = 0;

    bool arriving = false;
    void Update()
    {
        if(nbStops > 3)
        {
            // Level End, Go to level end scene
            GameManager.BetweenLevels();
        }
        
        //to manage if train is running or not
        if(!arriving && !interaction.isInteracting)
            internalClock += Time.deltaTime;

        if (internalClock > 5 && isMoving == false)
        {
            bb.Leave();
            isMoving = true;
            internalClock = 0;
        }
        if (internalClock > 10 && isMoving == true)
        {
            Debug.Log("Train stopped");

            isMoving = false;
            internalClock = 0;
            arriving = true;
            bb.Arrive();
            bb.OnFullStop.RemoveAllListeners();
            bb.OnFullStop.AddListener(() => OnStop());
        }
        //create passager while the train is stopped
        int nbNIOPass = (int)Random.Range(0, GameManager.NbNotInOrderPassenger);
        int nbIlePass = (int)Random.Range(0, GameManager.NbIlegalActionPassenger);
        
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

    private void OnStop()
    {
        numberOfPassengerLeaving = (int)Random.Range(0,actualNumberOfPassenger);
        //Debug.Log(numberOfPassengerLeaving + " passenger have left the train");
        actualNumberOfPassenger -= numberOfPassengerLeaving;
        numberOfPassengerComingIn = (int)Random.Range(0,numberOfPassenger - actualNumberOfPassenger);
        //Debug.Log(numberOfPassengerComingIn + " passenger come in the train");

        FindObjectOfType<Interaction>().EndInteract();

        nbStops++;
        arriving = false;
    }
}

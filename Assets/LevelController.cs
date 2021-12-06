using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    //metro states
    public bool isMoving = false;
    public int nbStops = 0;
    public bool arriving = false;

    //in sec
    private float internalClock = 0f;
    private float travelTime = 0f;
    public float timeBetweenStops = 8f;

    public GameObject passengerPrefab;
    private List<List<GameObject>> passengerWaves = new List<List<GameObject>>();
    private List<GameObject> passengerOnBoard = new List<GameObject>();

    [SerializeField] private Interaction interaction;
    // Metro doors position
    public Transform[] spawnPoints;

    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    [SerializeField] GameObject propartiPrefab;
    [SerializeField] GameObject migrantPrefab;
    [SerializeField] GameObject resistantePrefab;
    
    [SerializeField] private BackgroundBehaviour bb;

    [SerializeField] private ProgressBar pb;

    // Start is called before the first frame update
    private void Start() {
        passengerWaves = createPassengers(GameManager.NbPassenger, GameManager.NbNotInOrderPassenger, GameManager.NbIlegalActionPassenger);

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

        atStation();
    }
    
    void Update()
    {
        //update progress bar ui
        pb.progression = travelTime / (timeBetweenStops * 3);

        //pass time
        internalClock += Time.deltaTime;

        //if is moving -> pass travel time
        if (isMoving)
            travelTime += Time.deltaTime;

        //if has made a pause of X sec at station and not last station, leave station
        if (isMoving == false && nbStops < 4 && internalClock > 4)
        {
            Debug.Log("Train departure");
            bb.Leave();
            isMoving = true;
            internalClock = 0;
        }

        //if has moved for X sec, arrive at station
        else if (isMoving == true && internalClock > timeBetweenStops)
        {
            Debug.Log("Train stopping");

            isMoving = false;
            internalClock = 0;
            arriving = true;
            bb.Arrive();
            bb.OnFullStop.RemoveAllListeners();
            bb.OnFullStop.AddListener(() => atStation());
        }

        //if last stop and train arrived and not interacting -> end game
        if (nbStops > 3 && arriving == false && !interaction.isInteracting)
        {
            GameManager.BetweenLevels();
        }
    }

    private List<List<GameObject>> createPassengers(int nbPassenger, int nbNIOPass, int nbIlePass)
    {
        List<GameObject> passengers = new List<GameObject>();
        int NIO = nbNIOPass;
        int ILE = nbIlePass;

        for (int i = 0; i < nbPassenger; i++)
        {
            Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject passenger = Instantiate(passengerPrefab, spawnPoint, Quaternion.identity);
            passengers.Add(passenger);
            Vector2 endPosition = new Vector2(Random.Range(leftBound.position.x, rightBound.position.x), spawnPoint.y);
            passenger.GetComponent<Passenger>().position(spawnPoint, endPosition);
            if (nbNIOPass > 0)
            {
                //create a passenger not in order
                passenger.GetComponent<Passenger>().Init(false, false);
                NIO--;
            }
            else if (nbIlePass > 0)
            {
                //create a passenger that do illegal
                passenger.GetComponent<Passenger>().Init(true, true);
                ILE--;
            }
            else
            {
                //create a normal passenger
                passenger.GetComponent<Passenger>().Init(true, false);
            }

        }

        // shuffle passengers
        for (int i = 0; i < passengers.Count; i++)
        {
            int index = Random.Range(0, passengers.Count);
            GameObject temp = passengers[i];
            passengers[i] = passengers[index];
            passengers[index] = temp;
        }

        //create waves and add at least one passenger for each wave
        List<List<GameObject>> waves = new List<List<GameObject>>();
        for (int i = 0; i < 3; i++)
        {
            waves.Add(new List<GameObject>());
            waves[i].Add(passengers[0]);
            passengers.RemoveAt(0);
        }


        //randomly fill waves
        while (passengers.Count > 0)
        {
            int wave = Random.Range(0, 3);
            waves[wave].Add(passengers[0]);
            passengers.RemoveAt(0);
        }
        return waves;
    }

    private void atStation()
    {
        Debug.Log("Train at station");
        pb.arrivedNextStop();
        //make the passengers on board have a chance to leave
        if (nbStops < 3)
        {
            for (int i = passengerOnBoard.Count - 1; i > 0; i--)
            {
                float leaveChance = UnityEngine.Random.Range(0f, 1f);
                // if we are interracting with him, the passenger can't leave
                if (!(interaction.isInteracting && (interaction.interractedPassenger.Equals(passengerOnBoard[i]))) 
                    && leaveChance < 0.5)
                {
                    passengerOnBoard[i].GetComponent<Passenger>().leave();
                    passengerOnBoard.RemoveAt(i);
                }
            }

            //make the next wave arrive
            foreach (GameObject p in passengerWaves[nbStops])
            {
                passengerOnBoard.Add(p);
                p.GetComponent<Passenger>().arrive();
            }
        }
        else
        {
            //make everyone leave
            for (int i = passengerOnBoard.Count - 1; i > 0; i--)
            {
                passengerOnBoard[i].GetComponent<Passenger>().leave();
                passengerOnBoard.RemoveAt(i);
            }
        }
        arriving = false;
        nbStops++;
    }
}

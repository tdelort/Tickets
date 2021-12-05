using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] public GameObject passengerPrefab;

    enum MetroState {
        Moving,
        Stopped
    }

    enum TutoState {
        Waiting,
        Hello,
        GoodPassenger,
        BetweenPassengers,
        BadPassenger,
        End
    }

    public Transform[] spawnPoints;
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    //Dialogues
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Dialogue helloDialogue;
    [SerializeField] private Dialogue betweenDialogue;
    [SerializeField] private Dialogue endDialogue;

    [SerializeField] private BackgroundBehaviour bb;
    MetroState metroState = MetroState.Stopped;
    TutoState tutoState = TutoState.Waiting;

    float internalTimer = 0;
    GameObject passengerObj;
    Passenger passenger;
    /*
     *  Déroulement du tutoriel :
     *  - Un dialogue apparait pour introduire le joueur dans l'univers
     *  - Un passager en règle entre dans le metro, le joueur le controle
     *  - Un passager sans ticket entre, le joueur doit lui mettre une contravention.
     */

    void Update()
    {
        internalTimer += Time.deltaTime;
        switch (tutoState) {
            case TutoState.Waiting:
                tutoState = WaitingState();
                break;
            case TutoState.Hello:
                tutoState = HelloState();
                break;
            case TutoState.GoodPassenger:
                tutoState = GoodPassengerState();
                break;
            case TutoState.BetweenPassengers:
                tutoState = BetweenPassengersState();
                break;
            case TutoState.BadPassenger:
                tutoState = BadPassengerState();
                break;
            case TutoState.End:
                tutoState = EndState();
                break;
        }
    }
    
    private TutoState WaitingState()
    {
        if (internalTimer > 3)
        {
            internalTimer = 0;
            dialogueManager.StartDialogue(helloDialogue);
            foreach(GameObject o in bb.Doors)
            {
                o.GetComponent<DoorBehaviour>().DoorOpen();
            }
            return TutoState.Hello;
        }
        return TutoState.Waiting;
    }

    private TutoState HelloState()
    {
        if(dialogueManager.inDialogue)
            return TutoState.Hello;

        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        passengerObj = Instantiate(passengerPrefab, spawnPoint, Quaternion.identity);
        Vector2 endPosition = new Vector2(Random.Range(leftBound.position.x, rightBound.position.x), spawnPoint.y);

        passenger = passengerObj.GetComponent<Passenger>();
        passenger.position(spawnPoint, endPosition);
        passenger.Init(true, false);

        internalTimer = 0;
        metroState = MetroState.Moving;
        bb.Leave();
        return TutoState.GoodPassenger;
    }

    private TutoState GoodPassengerState()
    {
        if(internalTimer > 10)
        {
            internalTimer = 0;
            passenger.leave();
            metroState = MetroState.Stopped;
            dialogueManager.StartDialogue(betweenDialogue);
            bb.Arrive();
            return TutoState.BetweenPassengers;
        }
        return TutoState.GoodPassenger;
    }

    private TutoState BetweenPassengersState()
    {
        if(dialogueManager.inDialogue)
            return TutoState.BetweenPassengers;

        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        passengerObj = Instantiate(passengerPrefab, spawnPoint, Quaternion.identity);
        Vector2 endPosition = new Vector2(Random.Range(leftBound.position.x, rightBound.position.x), spawnPoint.y);

        passenger = passengerObj.GetComponent<Passenger>();
        passenger.position(spawnPoint, endPosition);
        passenger.Init(false, false);

        internalTimer = 0;
        metroState = MetroState.Moving;
        bb.Leave();
        return TutoState.BadPassenger;
    }

    private TutoState BadPassengerState()
    {
        if(internalTimer > 10)
        {
            internalTimer = 0;
            passenger.leave();
            metroState = MetroState.Stopped;
            dialogueManager.StartDialogue(endDialogue);
            bb.Arrive();
            return TutoState.End;
        }
        return TutoState.BadPassenger;
    }

    private TutoState EndState()
    {
        if(dialogueManager.inDialogue)
        {
            return TutoState.End;
        }
        GameManager.BetweenLevels();
        return TutoState.End;
    }

}

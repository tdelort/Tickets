using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLevelController : MonoBehaviour
{
    public GameObject passengerPrefab;
    // Start is called before the first frame update

    // Metro doors position
    public Transform[] spawnPoints;

    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;

    private List<GameObject> passengers = new List<GameObject>();
    private List<Vector2> startPositions = new List<Vector2>();
    private List<Vector2> endPositions = new List<Vector2>();

    void Start()
    {
        int nbNIOPass = GameData.NbNotInOrderPassenger;
        int nbIlePass = GameData.NbIlegalActionPassenger;

        for (int i = 0; i < GameData.NbPassenger; i++)
        {
            Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject passenger = Instantiate(passengerPrefab, spawnPoint, Quaternion.identity);
            passengers.Add(passenger);
            startPositions.Add(spawnPoint);

            Vector2 endPosition = new Vector2(Random.Range(leftBound.position.x, rightBound.position.x), spawnPoint.y);
            endPositions.Add(endPosition);

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
    }

    float elapsedTime = 0;
    float percentageCompleted = 0;
    float desiredDuration = 3f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        percentageCompleted = elapsedTime / desiredDuration;
        for (int i = 0; i < passengers.Count; i++)
        {
            passengers[i].transform.position = Vector3.Lerp(startPositions[i], endPositions[i], percentageCompleted);
        }
    }
}

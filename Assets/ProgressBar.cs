using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    public GameObject progressBar;
    [SerializeField]
    public GameObject[] Stops;

    //float from 0 to 1
    public float progression;

    private int nextStop = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 pos = new Vector3();
            pos.x = -5 + (progression * 10) / 2;
            progressBar.transform.localPosition = pos;

            Vector3 scal = new Vector3(0, 0.5f, 1);
            scal.x = progression * 10;
            progressBar.transform.localScale = scal;
        
    }

    public void arrivedNextStop()
    {
        if (nextStop < 3)
        {
            Stops[nextStop].SetActive(true);
            nextStop++;
        }
    }
}

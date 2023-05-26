using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [Header("Pertaining to Obstacle")]
    [SerializeField] private float spawnInterval;
    [SerializeField] private float randomRange;
    [SerializeField] private bool flightStatus;

    [Header("Pertaining to Player")]
    [SerializeField] private int numberofDeath;
    [SerializeField] private int jump;

    //Events 
    public event UnityAction RocketStageTrigger;
    public event UnityAction jumpTrigger;
    public event UnityAction uponDeath; 
    //for UI, Music, Score 

    public float SpawnInterval
    {
        get { return spawnInterval; }
        set { spawnInterval = value; }
    }

    public float RandomRange
    {
        get { return  randomRange; }
        set { randomRange = value; }
    }

    public bool FlightStatus
    {
        get { return flightStatus; }
        set
        {
            IntoRocketResponse(); 
            flightStatus = value;
        }
    }
    public int NumberofDeath
    {
        get { return numberofDeath; }
        set
        {
            numberofDeath = value;
        }
    }

    public int Jump
    {
        get { return jump; }
        set
        {
            jumpTrigger?.Invoke();
            jump = value;
        }
    }

    private void Start()
    {
        flightStatus = false;
        SpawnInterval = 1.5f;
        RandomRange = 2; 
    }



    public void IntoRocketResponse()
    {
        if (flightStatus)
        {
            flightStatus = false; 
        }
        else
        {
            flightStatus = true;
        }
        RocketStageTrigger?.Invoke();
    }
}

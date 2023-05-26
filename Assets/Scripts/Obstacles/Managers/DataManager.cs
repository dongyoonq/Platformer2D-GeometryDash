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

    private void Start()
    {
        flightStatus = false;
        SpawnInterval = 1.5f;
        RandomRange = 2; 
    }

    public event UnityAction RocketStageTrigger; 

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

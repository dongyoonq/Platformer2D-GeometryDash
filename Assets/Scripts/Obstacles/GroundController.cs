using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] public GameObject Groundcheck;
    [SerializeField] public bool groundorflight; 

    private void Start()
    {
        groundorflight = GameManager.Data.FlightStatus;
        GameManager.Data.RocketStageTrigger += GroundSwitch; 
        GroundSwitch(); 
    }
    private void GroundSwitch()
    {
        if (GameManager.Data.FlightStatus)
        {
            Groundcheck.SetActive(false);
        }
        else
        {
            Groundcheck.SetActive(true);
        }
    }
}

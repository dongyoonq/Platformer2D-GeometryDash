using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGeneratorController : MonoBehaviour
{
    public GameObject[] GeneratorList;

    private void Awake()
    {
        GameManager.Data.RocketStageTrigger += Switch; 
    }
    private void Switch()
    {
        foreach( GameObject go in GeneratorList )
        {
            if(go.activeSelf)
            {
                go.SetActive(false);
            }
            go.SetActive(true); 
        }
    }
}

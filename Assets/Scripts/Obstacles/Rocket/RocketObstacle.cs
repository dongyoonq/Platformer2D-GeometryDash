using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketObstacle : MonoBehaviour
{
    private void Start()
    {
        Invoke("SetForRuin", 5f); 
    }

    private void SetForRuin()
    {
        GameManager.Pool.ReturnBlockToPool(gameObject); 
    }
}

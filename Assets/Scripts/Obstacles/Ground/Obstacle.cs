using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void Start()
    {
        Invoke("SetforRuin", 5f); 
    }

    private void IdleState()
    {
        //대기만 한다. 
        return;
    }
    private void SetforRuin()
    {
        GameManager.Pool.ReturnBlockToPool(gameObject);
    }
}

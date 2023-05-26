using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Obstacle_Old : MonoBehaviour
{
    private float speed = 2.5f; // Speed at which the object moves
    private Transform endPoint; 
    public enum State
    {
        Idle, Launch
    }
    public State curState;

    private void Start()
    {
        curState = State.Idle;
        //이벤트 구문 추가 

    }
    private void Update()
    {
        if (curState == State.Idle)
            return;
        LaunchState();
    }

    private void SwitchState()
    {
        Debug.Log("Move!");
        curState = State.Launch;
    }

    private void IdleState()
    {
        //대기만 한다. 
        return;
    }

    private void LaunchState()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Check if the object reaches the endpoint
        if (transform.position.x <= endPoint.position.x)
        {
            GameManager.Pool.ReturnBlockToPool(gameObject);
        }
    }
}
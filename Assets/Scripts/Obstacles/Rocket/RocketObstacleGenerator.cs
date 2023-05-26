using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketObstacleGenerator : MonoBehaviour
{
    [SerializeField] private float spawnInterval;
    [SerializeField] private float randomRange;
    private Coroutine spawnRoutine;

    private void Start()
    {
        spawnInterval = GameManager.Data.SpawnInterval; 
        randomRange = GameManager.Data.RandomRange;
    }
    private void OnEnable()
    {
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(spawnRoutine);
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            transform.position = transform.position + Vector3.up * Random.Range(-randomRange, randomRange);
            GameManager.Pool.SetforRelease("RocketObstacle", transform); 
        }
    }
}

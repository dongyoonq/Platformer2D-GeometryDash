using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string DefaultName = "GameManager";

    private static GameManager instance;
    private static PoolManager poolManager;
    private static DataManager dataManager;

    public static GameManager Instance { get { return instance; } }
    public static PoolManager Pool { get { return poolManager; } }
    public static DataManager Data {  get { return dataManager; } }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("GameInstance: valid instance already registered.");
            Destroy(this);
            return;
        }

        instance = this;
        InitManagers(); 
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        InitGenerator();
    }
    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitGenerator()
    {
        GameObject poolObj = GameObject.FindGameObjectWithTag("Pool");
        poolObj.transform.SetParent(transform);
        poolManager = poolObj.GetComponent<PoolManager>();
    }

    private void InitManagers()
    {
        GameObject dataObj = new GameObject() { name = "Data Manager" };
        dataObj.transform.SetParent(transform);
        dataManager = dataObj.AddComponent<DataManager>();
    }
}

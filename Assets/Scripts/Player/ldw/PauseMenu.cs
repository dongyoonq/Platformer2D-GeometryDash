using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject go_BaseUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!UIManager.isPause)
                CallMenu();
            else
                CloseMenu();
        }
    }

    private void CallMenu()
    {
        UIManager.isPause = true;
        go_BaseUI.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()
    {
        UIManager.isPause = false;
        go_BaseUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

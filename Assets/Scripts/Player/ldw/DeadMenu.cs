using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject DeadUI;

    void Update()
    {
        if (UIManager.isDead)
        {
            CallMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    private void CallMenu()
    {
        UIManager.isPause = true;
        DeadUI.SetActive(true);
        Time.timeScale = 0f;
    }
    private void CloseMenu()
    {
        UIManager.isPause = false;
        DeadUI.SetActive(false);
        Time.timeScale = 1f;
    }
}

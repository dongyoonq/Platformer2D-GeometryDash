using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static bool isPause = false; // 메뉴가 호출되면 true
    public static bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPause || isDead)
        {
            // Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
        else
        {
            // Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
        }
    }
}

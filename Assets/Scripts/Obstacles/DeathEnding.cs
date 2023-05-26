using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEnding : MonoBehaviour
{
    Collider2D collider; 
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        Invoke("Activate", 2.5f); 
    }

    private void Activate()
    {
        collider.enabled = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player Dies 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGame : MonoBehaviour
{

    public static bool GameStart = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            GameStart = true;
        }
          
    }

}

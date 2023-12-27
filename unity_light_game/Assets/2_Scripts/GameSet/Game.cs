using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Game : MonoBehaviour

{
    public GameObject Panel;
    public GameObject player;
    public bool IsPause=false;
    public bool Gameover = false;
    public Vector3 ReturnPlace;
    void Start()
    {
        if (player != null) {
            player = GameObject.FindWithTag("player");
        }
        Panel.SetActive(false);
    }
    //gameover Ω√ »£√‚ 
    public void GameOver() {
        Gameover = true;

    }
    public void ReGame() {
        Gameover = false;
        player.transform.position = ReturnPlace;
    }
    public void SetReturn(Vector3 Place) {
        ReturnPlace = Place;
    }
    // Update is called once per frame
    void Update()
    {
        if (Gameover)
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
            

        }
        else {
            Panel.SetActive(false);
            Time.timeScale = 1;
        }
        //Debug.Log(ReturnPlace);
    }
}

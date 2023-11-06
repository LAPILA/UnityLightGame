using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Game : MonoBehaviour

{
    public GameObject Panel;
    public bool IsPause=false;
    public bool Gameover = false;

    void Start()
    {
        Panel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

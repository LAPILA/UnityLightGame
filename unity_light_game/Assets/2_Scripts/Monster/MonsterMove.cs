using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class MonsterMove : MonoBehaviour
{
    public float DistancePlayer = 0;
    public float DistanceReturn = 0;
    public float Speed = 3f;
    public float Recognition = 1f;
    public GameObject player;
    public GameObject Panel;
    public GameObject ReturnPlace;
    public Game game;
    private Transform MonsterTransform;
    private Rigidbody2D rigid;
    private bool Runflag = false;
    Vector3 direction;
    Vector3 Returndir;
    void Start()
    {
        
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (Panel == null)
        {
            Panel = GameObject.FindWithTag("Panel");
        }
        rigid = GetComponent<Rigidbody2D>();


    }
    void DetectedPlayer()
    {
        DistancePlayer = Vector3.Distance(player.transform.position, transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }
    void Detectedmyself() {
        DistanceReturn= Vector3.Distance(transform.position,ReturnPlace.transform.position);
        Returndir = ReturnPlace.transform.position - transform.position;
        Debug.Log(DistanceReturn);
        //Returndir.Normalize();
    }
    void Move()
    {
        if (DistancePlayer < Recognition)
        {
            transform.position += direction * Speed * Time.deltaTime;
        }
        else if (DistanceReturn > 0)
        {
            transform.position += Returndir * Speed*0.5f* Time.deltaTime;

        }
        else { rigid.velocity = Vector3.zero;}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Panel.SetActive(true);
            Time.timeScale = 0;
            //Debug.Log("충돌");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("충돌 벗어남");
    }
    // Update is called once per frame
    void Update()
    {
        Detectedmyself();
        DetectedPlayer();
        Move();
    }
}

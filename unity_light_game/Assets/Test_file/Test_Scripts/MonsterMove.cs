using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class MonsterMove : MonoBehaviour
{
    public float DistancePlayer = 0;
    public float Speed = 3f;
    public float Recognition = 1f;
    public GameObject player;
    public GameObject Panel;
    public Game game;
    private Transform MonsterTransform;
    private Rigidbody2D rigid;
    private bool flag=false;
    void Start() 
    {
        if (player == null) {
            player = GameObject.FindWithTag("Player");
        }
        if (Panel == null) {
            Panel = GameObject.FindWithTag("Panel");
        }
        rigid = GetComponent<Rigidbody2D>();
    }
    void DetectedPlayer ()
    {
        DistancePlayer = Vector2.Distance(player.transform.position,transform.position);
    }
    void Move() { 
        if (DistancePlayer < Recognition) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        }
        else{
            Debug.Log("정지");
            rigid.velocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            flag = true;
            Panel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("충돌");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        flag = false;
        Debug.Log("충돌 벗어남");
    }
    // Update is called once per frame
    void Update()
    {   
        DetectedPlayer();
        Move();
    }
}

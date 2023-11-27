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
    private Rigidbody2D rigid;
    Vector3 direction;
    Vector3 Returnplace;
    Vector3 Returndir;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Returnplace = new Vector3(transform.position.x, transform.position.y, transform.position.z);



    }
    void DetectedPlayer()
    {
        DistancePlayer = Vector3.Distance(player.transform.position, transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();
    }
    void Detectedmyself()
    {
        DistanceReturn = Vector3.Distance(transform.position, Returnplace);
        Returndir = Returnplace - transform.position;
        Debug.Log(DistanceReturn);
        Returndir.Normalize();
    }
    void Move()
    {
        if (DistancePlayer < Recognition)
        {
            transform.position += direction * Speed * Time.deltaTime;
        }
        else if (DistanceReturn > 0)
        {
            transform.position += Returndir * Speed * 0.5f * Time.deltaTime;

        }
        else { rigid.velocity = Vector3.zero; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            //Panel.SetActive(true);
            //Time.timeScale = 0;
            //Debug.Log("충돌");
        }
        else { rigid.velocity = Vector3.zero; }
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

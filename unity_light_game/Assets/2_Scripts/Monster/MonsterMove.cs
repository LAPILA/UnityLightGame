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
    public bool IsWalk = false;
    public bool IsReturn = false;
    public GameObject player;
    private Rigidbody2D rigid;
    private Animator animator;
    Vector3 direction;
    Vector3 Returnplace;
    Vector3 Returndir;
    AudioSource audioSource;
    public Game game;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        Returnplace = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        animator = GetComponent<Animator>();

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
        Returndir.Normalize();
    }
    void Move()
    {
        if (DistancePlayer < Recognition)
        {
            IsWalk = true;
            IsReturn = false;
            transform.position += direction * Speed * Time.deltaTime;
        }
        else if (DistanceReturn > 0)
        {
            IsReturn = true;
            transform.position += Returndir * Speed * 0.5f * Time.deltaTime;

        }
        else {
            IsWalk = false;
            IsReturn = false;
            rigid.velocity = Vector3.zero;
        }
    }
    void UpdateAnimation()
    {
        if (IsWalk != IsReturn)
        {
            if (direction.x < 0) { transform.localScale = new Vector3(-0.6f, 0.6f, 1); }
            else { transform.localScale = new Vector3(0.6f, 0.6f, 1); }
        }
        else if (IsWalk && IsReturn) {
            if (Returndir.x < 0) { transform.localScale = new Vector3(-0.6f, 0.6f, 1); }
            else { transform.localScale = new Vector3(0.6f, 0.6f, 1); }
        }
        audioSource.Play();
        animator.SetBool("isWALK",IsWalk);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            game.GameOver();
            Time.timeScale = 0;
            Debug.Log("충돌");
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
        UpdateAnimation();
    }
}

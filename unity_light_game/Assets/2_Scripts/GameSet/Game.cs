using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Game : MonoBehaviour
{
    public GameObject Panel;
    public GameObject player;
    public bool IsPause = false;
    public bool Gameover = false;
    public Vector3 ReturnPlace;
    

    private List<AudioSource> allAudioSources = new List<AudioSource>();

    void Start()
    {
        if (player != null) {
            player = GameObject.FindWithTag("Player");
        }
        Panel.SetActive(false);
        GetAllAudioSources();
    }

    // 게임이 종료될 때 모든 AudioSource를 멈추는 함수
    private void StopAllAudioSources()
    {
        foreach (var audioSource in allAudioSources) {
            audioSource.Stop();
        }
    }

    // 모든 AudioSource를 가져오는 함수
    private void GetAllAudioSources()
    {
        allAudioSources.Clear();
        var audioSources = FindObjectsOfType<AudioSource>();
        allAudioSources.AddRange(audioSources);
    }

    // gameover 시 호출 
    public void GameOver()
    {
        Gameover = true;
        StopAllAudioSources(); // 모든 사운드 멈추기
    }

    public void ReGame()
    {
        Gameover = false;
        player.transform.position = ReturnPlace;
        GetAllAudioSources(); // 모든 AudioSource 다시 가져오기
    }

    public void SetReturn(Vector3 Place)
    {
        ReturnPlace = Place;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gameover) {
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

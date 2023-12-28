using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Game : MonoBehaviour
{
    public GameObject Panel;
    public GameObject player;
    public GameObject song;
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
        StopAllAudioSources();
    }

    public void ReGame()
    {
        Gameover = false;
        player.transform.position = ReturnPlace;
        GetAllAudioSources(); 

        AudioSource songAudioSource = song.GetComponent<AudioSource>();

        if (songAudioSource != null) {
            songAudioSource.Stop(); // 먼저 중지
            songAudioSource.Play(); // 재시작
            songAudioSource.loop = true; // 루프 설정
        }
    }

    public void SetReturn(Vector3 Place)
    {
        ReturnPlace = Place;
    }

    // Update 함수를 사용하여 게임 전체를 정지시킵니다.
    void Update()
    {
        if (Gameover) {
            Time.timeScale = 0;
            Panel.SetActive(true);
            Debug.Log("게임오버");
        }
        else {
            Panel.SetActive(false);
            Debug.Log("재시작");
            
            Time.timeScale = 1;
        }
    }

}

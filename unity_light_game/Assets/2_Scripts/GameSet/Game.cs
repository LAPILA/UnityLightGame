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

    // ������ ����� �� ��� AudioSource�� ���ߴ� �Լ�
    private void StopAllAudioSources()
    {
        foreach (var audioSource in allAudioSources) {
            audioSource.Stop();
        }
    }

    // ��� AudioSource�� �������� �Լ�
    private void GetAllAudioSources()
    {
        allAudioSources.Clear();
        var audioSources = FindObjectsOfType<AudioSource>();
        allAudioSources.AddRange(audioSources);
    }

    // gameover �� ȣ�� 
    public void GameOver()
    {
        Gameover = true;
        StopAllAudioSources(); // ��� ���� ���߱�
    }

    public void ReGame()
    {
        Gameover = false;
        player.transform.position = ReturnPlace;
        GetAllAudioSources(); // ��� AudioSource �ٽ� ��������
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

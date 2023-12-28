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
        StopAllAudioSources();
    }

    public void ReGame()
    {
        Gameover = false;
        player.transform.position = ReturnPlace;
        GetAllAudioSources(); 

        AudioSource songAudioSource = song.GetComponent<AudioSource>();

        if (songAudioSource != null) {
            songAudioSource.Stop(); // ���� ����
            songAudioSource.Play(); // �����
            songAudioSource.loop = true; // ���� ����
        }
    }

    public void SetReturn(Vector3 Place)
    {
        ReturnPlace = Place;
    }

    // Update �Լ��� ����Ͽ� ���� ��ü�� ������ŵ�ϴ�.
    void Update()
    {
        if (Gameover) {
            Time.timeScale = 0;
            Panel.SetActive(true);
            Debug.Log("���ӿ���");
        }
        else {
            Panel.SetActive(false);
            Debug.Log("�����");
            
            Time.timeScale = 1;
        }
    }

}

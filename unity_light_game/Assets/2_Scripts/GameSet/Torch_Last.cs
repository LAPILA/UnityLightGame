using System.Collections;
using UnityEngine;

public class Torch_Last : MonoBehaviour
{
    AudioSource audioSource;
    int torch;
    bool audioplay = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        torch = PlayerMove.litTorches;

        if (torch >= 9) {
            Playonce();
            Invoke("DeactivateObject", 4f); // 2초 뒤에 DeactivateObject 함수 호출
        }
    }
    void Playonce()
    {
        if (torch >= 9 && !audioplay) {
            audioSource.Play();
            audioplay = true;   
        }
    }
    void DeactivateObject()
    {
        gameObject.SetActive(false); // 2초 뒤에 현재 게임 오브젝트 비활성화
    }
}

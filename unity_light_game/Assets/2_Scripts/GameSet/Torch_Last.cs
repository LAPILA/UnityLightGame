using System.Collections;
using UnityEngine;

public class Torch_Last : MonoBehaviour
{
    AudioSource audioSource;
    int torch;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        torch = PlayerMove.litTorches;

        if (torch >= 9) {
            audioSource.Play();
            Invoke("DeactivateObject", 2f); // 2초 뒤에 DeactivateObject 함수 호출
        }
    }

    void DeactivateObject()
    {
        gameObject.SetActive(false); // 2초 뒤에 현재 게임 오브젝트 비활성화
    }
}

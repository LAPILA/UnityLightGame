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
            Invoke("DeactivateObject", 4f); // 2�� �ڿ� DeactivateObject �Լ� ȣ��
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
        gameObject.SetActive(false); // 2�� �ڿ� ���� ���� ������Ʈ ��Ȱ��ȭ
    }
}

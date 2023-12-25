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
            Invoke("DeactivateObject", 2f); // 2�� �ڿ� DeactivateObject �Լ� ȣ��
        }
    }

    void DeactivateObject()
    {
        gameObject.SetActive(false); // 2�� �ڿ� ���� ���� ������Ʈ ��Ȱ��ȭ
    }
}

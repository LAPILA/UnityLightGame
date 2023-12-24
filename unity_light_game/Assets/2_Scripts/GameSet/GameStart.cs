using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI yourTMPElement;
    public float fadeDuration = 1f; // ���̵� ��/�ƿ��� �ɸ��� �ð�

    private CanvasGroup canvasGroup;
    private Coroutine currentCoroutine;

    void Start()
    {
        canvasGroup = yourTMPElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = yourTMPElement.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f; // �ʱ⿡�� �����ϰ� ����
        }

        StartCoroutine(FadeTMP(!canvasGroup.interactable, fadeDuration));
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene("MAP"); // �ƹ� Ű�� ������ ���� ������ �̵�
        }
    }

    IEnumerator FadeTMP(bool fadeIn, float duration)
    {
        while (true) {
            float targetAlpha = fadeIn ? 1f : 0f;
            float startAlpha = canvasGroup.alpha;
            float elapsedTime = 0f;

            while (elapsedTime < duration) {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;
            fadeIn = !fadeIn;
            yield return new WaitForSeconds(0.5f); // ���̵� ��/�ƿ� ����
        }
    }
}

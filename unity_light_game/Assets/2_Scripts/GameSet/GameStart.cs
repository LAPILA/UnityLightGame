using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI yourTMPElement;
    public float fadeDuration = 1f; // 페이드 인/아웃에 걸리는 시간

    private CanvasGroup canvasGroup;
    private Coroutine currentCoroutine;

    void Start()
    {
        canvasGroup = yourTMPElement.GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = yourTMPElement.gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f; // 초기에는 투명하게 설정
        }

        StartCoroutine(FadeTMP(!canvasGroup.interactable, fadeDuration));
    }

    void Update()
    {
        if (Input.anyKeyDown) {
            SceneManager.LoadScene("MAP"); // 아무 키나 누르면 다음 씬으로 이동
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
            yield return new WaitForSeconds(0.5f); // 페이드 인/아웃 간격
        }
    }
}

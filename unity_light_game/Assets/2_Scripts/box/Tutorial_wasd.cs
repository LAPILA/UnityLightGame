using UnityEngine;
using System.Collections;
public class Tutorial_wasd : MonoBehaviour
{
    private Renderer objectRenderer;
    private float fadeDuration = 4f;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.anyKeyDown) {
            StartCoroutine(FadeOutAndDestroyObject());
        }
    }

    private IEnumerator FadeOutAndDestroyObject()
    {
        float elapsedTime = 0f;
        Color objectColor = objectRenderer.material.color;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeDuration;
            objectColor.a = Mathf.Lerp(1f, 0f, normalizedTime);

            if (objectRenderer.material.HasProperty("_Color")) {
                objectRenderer.material.color = objectColor;
            }
            else if (objectRenderer.material.HasProperty("_TintColor")) {
                objectRenderer.material.SetColor("_TintColor", objectColor);
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float staminaDecreaseRate = 1f; // 초당 스태미나 감소율
    public float staminaIncreaseRate = 1f; // 초당 스태미나 회복율
    public float decreaseInterval = 1f; // 스태미나 감소 간격
    public float decreaseDelay = 1.5f; // 스태미나 감소 딜레이
    public float increaseDelay = 0.5f; // 스태미나 회복 딜레이
    public Light2D playerLight; // 플레이어 Light2D
    public Scrollbar staminaScrollbar; // UI Scrollbar

    private float currentStamina; // 현재 스태미나 양
    private bool isLightOn = true; // Light2D 상태를 나타내는 플래그
    private Coroutine staminaDecreaseCoroutine; // 스태미나 감소 Coroutine을 저장할 변수
    private Coroutine staminaIncreaseCoroutine; // 스태미나 회복 Coroutine을 저장할 변수

    private void Start()
    {
        currentStamina = maxStamina; // 초기 스태미나 설정
        UpdateStaminaUI(); // UI 업데이트
        StartCoroutine(StaminaDecreaseRoutine()); // 스태미나 감소 루틴 시작
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            ToggleLight(); // F 키를 누르면 Light2D를 토글합니다.
        }
    }

    private void ToggleLight()
    {
        isLightOn = !isLightOn; // Light2D 상태를 토글합니다.
        playerLight.enabled = isLightOn; // Light2D를 켜거나 끕니다.

        // 스태미나 감소 Coroutine을 중지합니다.
        if (staminaDecreaseCoroutine != null) {
            StopCoroutine(staminaDecreaseCoroutine);
        }

        // 스태미나 회복 Coroutine을 중지합니다.
        if (staminaIncreaseCoroutine != null) {
            StopCoroutine(staminaIncreaseCoroutine);
        }

        if (isLightOn) {
            // Light2D가 켜진 경우 스태미나 감소 루틴을 시작합니다.
            staminaDecreaseCoroutine = StartCoroutine(StaminaDecreaseRoutine());
        }
        else {
            // Light2D가 꺼진 경우 스태미나 회복 루틴을 시작합니다.
            staminaIncreaseCoroutine = StartCoroutine(StaminaIncreaseRoutine());
        }
    }

    private IEnumerator StaminaIncreaseRoutine()
    {
        yield return new WaitForSeconds(increaseDelay); // 스태미나 회복 딜레이

        while (currentStamina < maxStamina) {
            currentStamina += staminaIncreaseRate; // 스태미나를 증가시킵니다.
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina); // 스태미나가 최대값을 넘지 않도록 제한합니다.
            UpdateStaminaUI(); // UI 업데이트
            yield return new WaitForSeconds(1f); // 1초마다 스태미나를 증가시킵니다.
        }
    }

    private IEnumerator StaminaDecreaseRoutine()
    {
        yield return new WaitForSeconds(decreaseDelay); // 스태미나 감소 딜레이

        while (true) {
            if (isLightOn) {
                if (currentStamina > 0) {
                    currentStamina -= staminaDecreaseRate; // 스태미나를 감소시킵니다.
                    currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina); // 스태미나가 최소값 미만으로 떨어지지 않도록 제한합니다.
                    UpdateStaminaUI(); // UI 업데이트
                }
                else {
                    isLightOn = false; // 스태미나가 모두 소진되면 Light2D를 꺼둡니다.
                    playerLight.enabled = false;
                    // 스태미나가 0인 경우 스태미나 회복 루틴을 시작합니다.
                    staminaIncreaseCoroutine = StartCoroutine(StaminaIncreaseRoutine());
                }
            }
            yield return new WaitForSeconds(decreaseInterval); // 주기적으로 스태미나를 감소시킵니다.
        }
    }

    private void UpdateStaminaUI()
    {
        // UI Scrollbar의 값을 업데이트하여 스태미나를 표시합니다.
        staminaScrollbar.size = currentStamina / maxStamina;
    }
}

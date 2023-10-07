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
    private bool isDecreasing = true; // 스태미나 감소 중인지 나타내는 플래그

    private void Start()
    {
        currentStamina = maxStamina; // 초기 스태미나 설정
        staminaDecreaseCoroutine = StartCoroutine(StaminaDecreaseRoutine()); // 스태미나 감소 루틴 시작
        UpdateStaminaUI(); // UI 업데이트
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log("F");
            ToggleLight(); // F 키를 누르면 Light2D를 토글
            
        }

    }

    private void ToggleLight()
    {
        isLightOn = !isLightOn; // Light2D 상태를 토글
        playerLight.enabled = isLightOn; // Light2D on/off

        // 스태미나 감소 Coroutine을 중지
        if (staminaDecreaseCoroutine != null) {
            Debug.Log("스태미나 감소 중지");
            StopCoroutine(staminaDecreaseCoroutine);
            isDecreasing = false; // 감소 중인 상태를 해제
        }

        // 스태미나 회복 Coroutine을 중지
        if (staminaIncreaseCoroutine != null) {
            Debug.Log("스태미나 회복 중지");
            StopCoroutine(staminaIncreaseCoroutine);
        }

        if (isLightOn) {
            // Light2D가 켜진 경우 스태미나 감소 루틴을 시작
            if (!isDecreasing) {
                Debug.Log("감소 중");
                staminaDecreaseCoroutine = StartCoroutine(StaminaDecreaseRoutine());
                isDecreasing = true; // 감소 중인 상태 설정
            }
        }
        else if (!isDecreasing) {
            // Light2D가 꺼진 경우 스태미나 회복 루틴을 시작 (감소 중이 아닌 경우에만)
            Debug.Log("회복중");
            staminaIncreaseCoroutine = StartCoroutine(StaminaIncreaseRoutine());
        }
    }

    private IEnumerator StaminaIncreaseRoutine()
    {
        yield return new WaitForSeconds(increaseDelay); // 스태미나 회복 딜레이

        while (currentStamina < maxStamina && !isDecreasing) { // isDecreasing 플래그를 추가하여 감소 중이 아닌 경우에만 실행
            currentStamina += staminaIncreaseRate; // 스태미나를 증가
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina); // 스태미나가 최대값을 넘지 않도록 제한
            UpdateStaminaUI(); // UI 업데이트
            yield return new WaitForSeconds(1f); // 1초마다 스태미나를 증가
        }
    }

    private IEnumerator StaminaDecreaseRoutine()
    {
        yield return new WaitForSeconds(decreaseDelay); // 스태미나 감소 딜레이

        while (true) {
            if (isLightOn && isDecreasing) { // isDecreasing 플래그를 추가하여 감소 중인지 확인
                if (currentStamina > 0.4) {
                    currentStamina -= staminaDecreaseRate; // 스태미나를 감소
                    currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina); // 스태미나가 최소값 미만으로 떨어지지 않도록 제한
                    UpdateStaminaUI(); // UI 업데이트
                }
                else {
                    isLightOn = false; // 스태미나가 모두 소진되면 Light2D off
                    playerLight.enabled = false;
                    isDecreasing = false;
                    // 스태미나가 0인 경우 스태미나 회복 루틴을 시작
                    staminaIncreaseCoroutine = StartCoroutine(StaminaIncreaseRoutine());
                    
                }
            }
            yield return new WaitForSeconds(decreaseInterval); // 주기적으로 스태미나를 감소
        }
    }

    private void UpdateStaminaUI()
    {
        // UI Scrollbar의 값을 업데이트하여 스태미나를 표시
        staminaScrollbar.size = currentStamina / maxStamina;
        Debug.Log(staminaScrollbar.size);
    }
}

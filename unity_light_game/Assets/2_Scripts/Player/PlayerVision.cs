using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;  // for Light2D
using UnityEngine.Rendering.Universal;

public class PlayerVision : MonoBehaviour
{
    public static PlayerVision Instance;
    public Light2D playerLight;  // 라이트 컴포넌트
    public int totalTorches = 8;  // 총 등불 수
    private int litTorches = 0;  // 현재 켜진 등불 수

    private float initialOuterRadius = 1f;  // 초기 outerRadius 값
    private float initialInnerRadius = 1f;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 초기 outerRadius 값을 저장합니다.
        initialOuterRadius = playerLight.pointLightOuterRadius;
        initialInnerRadius = playerLight.pointLightInnerRadius;
    }

    public void LightTorch()
    {
        Debug.Log("LightTorch Called");
        litTorches++;  // 등불 하나를 켭니다.
        UpdateVision();  // 시야 범위를 업데이트합니다.
    }

    void UpdateVision()
    {
        // 등불을 하나 켰을 때 innerRadius와 outerRadius를 0.1씩 줄입니다.
        float newInnerRadius = playerLight.pointLightInnerRadius - 0.1f;
        float newOuterRadius = playerLight.pointLightOuterRadius - 0.1f;

        // 새로운 Radius 값을 설정합니다.
        playerLight.pointLightInnerRadius = newInnerRadius;
        playerLight.pointLightOuterRadius = newOuterRadius;
    }

}

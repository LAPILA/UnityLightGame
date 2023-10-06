using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    private Light2D torchLight;
    private bool interacted = false;

    private void Start()
    {
        // Torch 오브젝트 내에 있는 Light2D 컴포넌트 찾기
        torchLight = GetComponentInChildren<Light2D>();
        // 초기에 빛 비활성화
        torchLight.intensity = 0f;
    }

    public bool IsInteracted()
    {
        return interacted;
    }

    public void ActivateTorch()
    {
        if (!interacted) {
            // 횃불을 활성화하고 빛을 켭니다.
            torchLight.intensity = 1f;
            // Torch 오브젝트를 비활성화하거나 제거할 수도 있습니다.
            interacted = true; // 횃불 상호 작용 플래그 설정
        }
    }

    // SetInteracted 메서드 추가
    public void SetInteracted(bool value)
    {
        interacted = value;
    }
}

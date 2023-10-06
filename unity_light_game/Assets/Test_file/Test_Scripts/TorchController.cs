using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class TorchController : MonoBehaviour
{
    private Light2D torchLight;
    private bool interacted = false;

    private void Start()
    {
        // Torch ������Ʈ ���� �ִ� Light2D ������Ʈ ã��
        torchLight = GetComponentInChildren<Light2D>();
        // �ʱ⿡ �� ��Ȱ��ȭ
        torchLight.intensity = 0f;
    }

    public bool IsInteracted()
    {
        return interacted;
    }

    public void ActivateTorch()
    {
        if (!interacted) {
            // ȶ���� Ȱ��ȭ�ϰ� ���� �մϴ�.
            torchLight.intensity = 1f;
            // Torch ������Ʈ�� ��Ȱ��ȭ�ϰų� ������ ���� �ֽ��ϴ�.
            interacted = true; // ȶ�� ��ȣ �ۿ� �÷��� ����
        }
    }

    // SetInteracted �޼��� �߰�
    public void SetInteracted(bool value)
    {
        interacted = value;
    }
}

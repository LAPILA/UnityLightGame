using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;  // for Light2D
using UnityEngine.Rendering.Universal;

public class PlayerVision : MonoBehaviour
{
    public static PlayerVision Instance;
    public Light2D playerLight;  // ����Ʈ ������Ʈ
    public int totalTorches = 8;  // �� ��� ��
    private int litTorches = 0;  // ���� ���� ��� ��

    private float initialOuterRadius = 1f;  // �ʱ� outerRadius ��
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
        // �ʱ� outerRadius ���� �����մϴ�.
        initialOuterRadius = playerLight.pointLightOuterRadius;
        initialInnerRadius = playerLight.pointLightInnerRadius;
    }

    public void LightTorch()
    {
        Debug.Log("LightTorch Called");
        litTorches++;  // ��� �ϳ��� �մϴ�.
        UpdateVision();  // �þ� ������ ������Ʈ�մϴ�.
    }

    void UpdateVision()
    {
        // ����� �ϳ� ���� �� innerRadius�� outerRadius�� 0.1�� ���Դϴ�.
        float newInnerRadius = playerLight.pointLightInnerRadius - 0.1f;
        float newOuterRadius = playerLight.pointLightOuterRadius - 0.1f;

        // ���ο� Radius ���� �����մϴ�.
        playerLight.pointLightInnerRadius = newInnerRadius;
        playerLight.pointLightOuterRadius = newOuterRadius;
    }

}

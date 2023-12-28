using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;  // for Light2D
using UnityEngine.Rendering.Universal;

public class PlayerVision : MonoBehaviour
{
    public static PlayerVision Instance;
    public Light2D playerLight;  // ����Ʈ ������Ʈ
    public int totalTorches = 18;  // �� ��� ��
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
        // �ʱ� outerRadius ���� ����
        initialOuterRadius = playerLight.pointLightOuterRadius;
        initialInnerRadius = playerLight.pointLightInnerRadius;
    }

    public void LightTorch()
    {
        Debug.Log("LightTorch Called");
        litTorches++;  // ��� �ϳ��� ��
        UpdateVision();  // �þ� ������ ������Ʈ
    }

    void UpdateVision()
    {
        float decreaseAmount = 0.05f;

        // ����� �ϳ� ���� �� innerRadius�� outerRadius��0.05�� ����
        float newInnerRadius = Mathf.Max(playerLight.pointLightInnerRadius - decreaseAmount, 0.2f);
        float newOuterRadius = Mathf.Max(playerLight.pointLightOuterRadius - decreaseAmount, 0.4f);

        // ���ο� Radius ���� �����մϴ�.
        playerLight.pointLightInnerRadius = newInnerRadius;
        playerLight.pointLightOuterRadius = newOuterRadius;

    }

}

using UnityEngine;
using System.Collections;

public class PlayerItems : MonoBehaviour
{
    public float glowBagDuration = 10f; // Inspector���� ���� �����ϵ��� public���� ����
    public float glowBagCooldown = 10f; // GlowBag ��Ÿ�� �ð�
    private float glowBagCooldownTime = 0f; // ��Ÿ���� üũ�� ����
    public GameObject beaconPrefab; // Inspector���� Beacon �������� �Ҵ��� �� �ֵ��� public ������ ����

    // PlayerMove�� PlayerVision ������Ʈ�� ���� public ����
    public PlayerMove playerMove;
    public PlayerVision playerVision;

    private void Start()
    {
        // ������Ʈ�� �Ҵ���� �ʾ��� ���, GameObject���� �ڵ����� ã�� �Ҵ�
        if (!playerMove) playerMove = GetComponent<PlayerMove>();
        if (!playerVision) playerVision = GetComponent<PlayerVision>();
    }

    private void Update()
    {
        // ���� Ű �Է��� Ȯ���ϰ� �ش� ������ ���
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            UseBread();
        }
        else if (Time.time > glowBagCooldownTime) {
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                StartCoroutine(UseGlowBag());
                // ���� �ð��� ��Ÿ���� ���� ���� ��� ���� �ð��� ������Ʈ
                glowBagCooldownTime = Time.time + glowBagCooldown;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            PlaceBeacon();
        }
    }

    private void UseBread()
    {
        // ���� ����� ���� ȿ���� ����
        playerMove.RecoverStamina(10f);
    }

    private IEnumerator UseGlowBag()
    {
        //GlowBag ���� ���� TorchRight ���� ���� �Ȱ����ϴ�  �Լ� �ʼ�
        // GlowBag ȿ�� ����
        float originalOuterRadius = playerVision.playerLight.pointLightOuterRadius;
        playerVision.playerLight.pointLightOuterRadius *= 2f; // �þ� ���� 2�� ����

        yield return new WaitForSeconds(glowBagDuration); // ���� �ð� ���� ��ٸ�

        playerVision.playerLight.pointLightOuterRadius = originalOuterRadius; // �þ� ���� ����
    }

    private void PlaceBeacon()
    {
        // Beacon ��ġ�� �÷��̾� ���� ��ġ�� ��ġ
        Instantiate(beaconPrefab, transform.position, Quaternion.identity);
    }
}

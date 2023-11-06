using UnityEngine;
using System.Collections;

public class PlayerItems : MonoBehaviour
{
    public float glowBagDuration = 10f; // Inspector에서 수정 가능하도록 public으로 변경
    public float glowBagCooldown = 10f; // GlowBag 쿨타임 시간
    private float glowBagCooldownTime = 0f; // 쿨타임을 체크할 변수
    public GameObject beaconPrefab; // Inspector에서 Beacon 프리팹을 할당할 수 있도록 public 변수로 선언

    // PlayerMove와 PlayerVision 컴포넌트에 대한 public 참조
    public PlayerMove playerMove;
    public PlayerVision playerVision;

    private void Start()
    {
        // 컴포넌트가 할당되지 않았을 경우, GameObject에서 자동으로 찾아 할당
        if (!playerMove) playerMove = GetComponent<PlayerMove>();
        if (!playerVision) playerVision = GetComponent<PlayerVision>();
    }

    private void Update()
    {
        // 숫자 키 입력을 확인하고 해당 아이템 사용
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            UseBread();
        }
        else if (Time.time > glowBagCooldownTime) {
            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                StartCoroutine(UseGlowBag());
                // 현재 시간에 쿨타임을 더해 다음 사용 가능 시간을 업데이트
                glowBagCooldownTime = Time.time + glowBagCooldown;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            PlaceBeacon();
        }
    }

    private void UseBread()
    {
        // 빵을 사용할 때의 효과를 실행
        playerMove.RecoverStamina(10f);
    }

    private IEnumerator UseGlowBag()
    {
        //GlowBag 실행 도중 TorchRight 감소 영향 안가게하는  함수 필수
        // GlowBag 효과 실행
        float originalOuterRadius = playerVision.playerLight.pointLightOuterRadius;
        playerVision.playerLight.pointLightOuterRadius *= 2f; // 시야 범위 2배 증가

        yield return new WaitForSeconds(glowBagDuration); // 지속 시간 동안 기다림

        playerVision.playerLight.pointLightOuterRadius = originalOuterRadius; // 시야 범위 복구
    }

    private void PlaceBeacon()
    {
        // Beacon 위치를 플레이어 현재 위치에 배치
        Instantiate(beaconPrefab, transform.position, Quaternion.identity);
    }
}

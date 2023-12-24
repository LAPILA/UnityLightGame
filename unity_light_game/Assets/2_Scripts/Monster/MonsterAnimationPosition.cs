using UnityEngine;

public class MonsterAnimationPosition : MonoBehaviour
{
    private Vector3 originalPosition; // 몬스터의 원래 위치
    private bool isPlayerInRange; // 플레이어가 범위 안에 있는지 여부 확인하는 변수
    private float timeInRange; // 플레이어가 범위 안에 머무른 시간

    private Animator animator; // 몬스터의 애니메이터

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
        originalPosition = transform.position; // 몬스터의 초기 위치 기억
        isPlayerInRange = false; // 초기에는 플레이어가 범위 안에 없음
        timeInRange = 0f; // 초기에는 시간은 0으로 설정
    }

    private void Update()
    {
        // 플레이어가 범위 안에 있고, 시간이 1.5초 이상이면 공격 애니메이션 시작
        if (isPlayerInRange) {
            timeInRange += Time.deltaTime; // 시간을 누적

            if (timeInRange >= 1.5f) {
                animator.SetBool("isAttacking", true); // Attack 애니메이션 시작
            }
        }
        else {
            timeInRange = 0f; // 플레이어가 범위를 벗어나면 시간 초기화
            animator.SetBool("isAttacking", false); // Attack 애니메이션 종료
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // 플레이어와의 충돌 체크
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isPlayerInRange = true; // 플레이어가 범위 안에 있다고 표시
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어와의 충돌 끝 체크
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isPlayerInRange = false; // 플레이어가 범위를 벗어났다고 표시
        }
    }
}

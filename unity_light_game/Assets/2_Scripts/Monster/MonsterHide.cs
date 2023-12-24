using System.Collections;
using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    [SerializeField] CircleCollider2D Light_radius; // 플레이어의 빛 감지 콜라이더
    [SerializeField] BoxCollider2D Monster_box; // 몬스터의 콜라이더
    [SerializeField] BoxCollider2D AttackRangeCollider; // 공격 범위 콜라이더

    private bool isInLight = false; // 빛에 노출되었는지 여부
    private bool isAttacking = false; // 공격 중인지 여부

    private Animator animator; // 몬스터의 애니메이터

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
        AttackRangeCollider.enabled = false; // AttackRange Collider 초기에 비활성화
    }

    private void Update()
    {
        if (isAttacking) {
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = true; // 빛에 노출됨
            StartAttack(); // 공격 시작
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = false; // 빛에서 벗어남
            StopAttack(); // 공격 중지
        }
    }

    private void StartAttack()
    {
        isAttacking = true; // 공격 상태로 변경
        animator.SetBool("isAttacking", true); // 애니메이션 상태 변경
    }

    private void StopAttack()
    {
        isAttacking = false; // 공격 종료
        animator.SetBool("isAttacking", false); // 애니메이션 상태 변경
    }

    private void GameOver()
    {
        Debug.Log("게임 오버!"); // 여기에 게임 오버 로직을 추가하세요
        // 게임 오버 처리 등의 코드를 작성하세요
    }
}

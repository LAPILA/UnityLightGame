using System.Collections;
using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    [SerializeField] CircleCollider2D Light_radius; // 플레이어의 빛 감지 콜라이더
    [SerializeField] BoxCollider2D Monster_box; // 몬스터의 콜라이더
    [SerializeField] private float attackRange = 3f; // Serialized 필드: 공격 범위
    [SerializeField] private float attackDelay = 0.2f; // Serialized 필드: 공격 딜레이

    private bool isInLight = false; // 빛에 노출되었는지 여부
    private bool isAttacking = false; // 공격 중인지 여부

    private Animator animator; // 몬스터의 애니메이터

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
    }

    private void Update()
    {
        // 빛에 노출되었고 빛과 몬스터 콜라이더가 겹치는 경우
        if (isInLight && Monster_box.IsTouching(Light_radius)) {
            StartCoroutine(AttackAfterDelay(attackDelay)); // 공격 딜레이 후 공격 시작
            if(isAttacking) {
                AttackInArea(); // 특정 범위 내의 플레이어 공격 함수 호출
            }
        }
        else // 빛에 노출되지 않거나 겹치지 않는 경우
        {
            StopContinuousAttack(); // 공격 중지
        }
    }

    // 공격 시작 딜레이 후에 공격하는 코루틴
    private IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 공격 딜레이 대기
        StartContinuousAttack(); // 지속적인 공격 시작
    }

    // 공격 시작
    private void StartContinuousAttack()
    {
        if (isInLight && !isAttacking) // 빛에 노출되고 공격 중이 아닌 경우
        {
            isAttacking = true; // 공격 상태로 변경
            animator.SetBool("isAttacking", true); // 애니메이션 상태 변경
            Debug.Log("공격 중!");
        }
    }

    // 공격 중지
    private void StopContinuousAttack()
    {
        if ((!isInLight || !Monster_box.IsTouching(Light_radius)) && isAttacking) // 빛에서 벗어나거나 빛과 몬스터 콜라이더가 겹치지 않으면서 공격 중인 경우
        {
            isAttacking = false; // 공격 종료
            isInLight = false;
            animator.SetBool("isAttacking", false); // 애니메이션 상태 변경
            Debug.Log("휴식 중!");
        }
    }

    // 플레이어 빛 콜라이더와 충돌한 경우
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = true; // 빛에 노출됨
        }
    }

    // 플레이어 빛 콜라이더와의 충돌이 끝난 경우
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = false; // 빛에서 벗어남
            StopContinuousAttack(); // 공격 중지
        }
    }

    // 특정 범위 내의 플레이어 공격
    private void AttackInArea()
    {
        Vector2 monsterPosition = transform.position;
        Vector2 attackDirection = transform.right;

        // 공격 범위 그리기
        Debug.DrawRay(monsterPosition, attackDirection * attackRange, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(monsterPosition, attackDirection, attackRange); // 몬스터의 오른쪽으로 Raycast
        if (hit.collider != null && hit.collider.CompareTag("Player")) // 충돌한 것이 플레이어인 경우
        {
            Debug.Log("플레이어를 공격합니다!");
            // 플레이어에게 데미지를 줄 수 있는 함수 호출
        }
    }
}

using UnityEngine;

public class MonsterAnimationPosition : MonoBehaviour
{
    private Vector3 originalPosition; // 몬스터의 원래 위치

    private Animator animator; // 몬스터의 애니메이터

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메이터 가져오기
        originalPosition = transform.position; // 몬스터의 초기 위치 기억
    }

    public void StartAttackAnimation()
    {
        // Attack 애니메이션 시작할 때 위치 기억
        originalPosition = transform.position;
        // 이곳에 Attack 애니메이션 시작 코드 추가
        animator.SetBool("isAttacking", true); // Attack 애니메이션 시작
    }

    public void EndAttackAnimation()
    {
        // Attack 애니메이션이 끝나면 원래 위치로 되돌림
        animator.SetBool("isAttacking", false); // Attack 애니메이션 종료
        transform.position = originalPosition; // 원래 위치로 돌아감
    }
}

using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    public Transform player; // 플레이어 Transform
    public float attackRange = 3f; // 공격 범위 (조절)
    public float detectionRange = 7f; // 감지 범위 (조절)
    public float moveSpeed = 2f; // 이동 속도 (조절)

    private Animator animator; // 애니메이터 컴포넌트
    private bool isDead = false; // 사망 상태
    private bool isAttacking = false; // 공격 중인지 여부

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // 사망 상태일 경우 아무 동작도 하지 않음

        DetectPlayer();
        UpdateAnimationState();
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange) {
            // 플레이어가 공격 범위 안에 있을 때
            isAttacking = true;
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", true);
        }
        else if (distanceToPlayer <= detectionRange) {
            // 플레이어가 감지 범위 안에 있을 때
            if (!isAttacking) {
                animator.SetBool("isWalk", true);
            }
            animator.SetBool("isAttack", false);
        }
        else {
            // 플레이어가 범위 밖에 있을 때
            isAttacking = false;
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
        }

        MoveTowardsPlayer(distanceToPlayer);
    }

    void MoveTowardsPlayer(float distanceToPlayer)
    {
        if (animator.GetBool("isWalk") && distanceToPlayer > attackRange) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void UpdateAnimationState()
    {
        // 필요한 경우 다른 애니메이션 상태 업데이트
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torch") && !isDead) {
            isDead = true;
            isAttacking = false;
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isDead", true);
            // 여기서 추가적인 사망 로직을 구현할 수 있음
        }
    }
}

using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    public Transform player; // �÷��̾� Transform
    public float attackRange = 3f; // ���� ���� (����)
    public float detectionRange = 7f; // ���� ���� (����)
    public float moveSpeed = 2f; // �̵� �ӵ� (����)

    private Animator animator; // �ִϸ����� ������Ʈ
    private bool isDead = false; // ��� ����
    private bool isAttacking = false; // ���� ������ ����

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return; // ��� ������ ��� �ƹ� ���۵� ���� ����

        DetectPlayer();
        UpdateAnimationState();
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange) {
            // �÷��̾ ���� ���� �ȿ� ���� ��
            isAttacking = true;
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", true);
        }
        else if (distanceToPlayer <= detectionRange) {
            // �÷��̾ ���� ���� �ȿ� ���� ��
            if (!isAttacking) {
                animator.SetBool("isWalk", true);
            }
            animator.SetBool("isAttack", false);
        }
        else {
            // �÷��̾ ���� �ۿ� ���� ��
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
        // �ʿ��� ��� �ٸ� �ִϸ��̼� ���� ������Ʈ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torch") && !isDead) {
            isDead = true;
            isAttacking = false;
            animator.SetBool("isWalk", false);
            animator.SetBool("isAttack", false);
            animator.SetBool("isDead", true);
            // ���⼭ �߰����� ��� ������ ������ �� ����
        }
    }
}

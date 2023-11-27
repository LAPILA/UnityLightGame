using System.Collections;
using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    [SerializeField] CircleCollider2D Light_radius; // �÷��̾��� �� ���� �ݶ��̴�
    [SerializeField] BoxCollider2D Monster_box; // ������ �ݶ��̴�
    [SerializeField] private float attackRange = 3f; // Serialized �ʵ�: ���� ����
    [SerializeField] private float attackDelay = 0.2f; // Serialized �ʵ�: ���� ������

    private bool isInLight = false; // ���� ����Ǿ����� ����
    private bool isAttacking = false; // ���� ������ ����

    private Animator animator; // ������ �ִϸ�����

    private void Awake()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ��������
    }

    private void Update()
    {
        // ���� ����Ǿ��� ���� ���� �ݶ��̴��� ��ġ�� ���
        if (isInLight && Monster_box.IsTouching(Light_radius)) {
            StartCoroutine(AttackAfterDelay(attackDelay)); // ���� ������ �� ���� ����
            if(isAttacking) {
                AttackInArea(); // Ư�� ���� ���� �÷��̾� ���� �Լ� ȣ��
            }
        }
        else // ���� ������� �ʰų� ��ġ�� �ʴ� ���
        {
            StopContinuousAttack(); // ���� ����
        }
    }

    // ���� ���� ������ �Ŀ� �����ϴ� �ڷ�ƾ
    private IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� ������ ���
        StartContinuousAttack(); // �������� ���� ����
    }

    // ���� ����
    private void StartContinuousAttack()
    {
        if (isInLight && !isAttacking) // ���� ����ǰ� ���� ���� �ƴ� ���
        {
            isAttacking = true; // ���� ���·� ����
            animator.SetBool("isAttacking", true); // �ִϸ��̼� ���� ����
            Debug.Log("���� ��!");
        }
    }

    // ���� ����
    private void StopContinuousAttack()
    {
        if ((!isInLight || !Monster_box.IsTouching(Light_radius)) && isAttacking) // ������ ����ų� ���� ���� �ݶ��̴��� ��ġ�� �����鼭 ���� ���� ���
        {
            isAttacking = false; // ���� ����
            isInLight = false;
            animator.SetBool("isAttacking", false); // �ִϸ��̼� ���� ����
            Debug.Log("�޽� ��!");
        }
    }

    // �÷��̾� �� �ݶ��̴��� �浹�� ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = true; // ���� �����
        }
    }

    // �÷��̾� �� �ݶ��̴����� �浹�� ���� ���
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = false; // ������ ���
            StopContinuousAttack(); // ���� ����
        }
    }

    // Ư�� ���� ���� �÷��̾� ����
    private void AttackInArea()
    {
        Vector2 monsterPosition = transform.position;
        Vector2 attackDirection = transform.right;

        // ���� ���� �׸���
        Debug.DrawRay(monsterPosition, attackDirection * attackRange, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(monsterPosition, attackDirection, attackRange); // ������ ���������� Raycast
        if (hit.collider != null && hit.collider.CompareTag("Player")) // �浹�� ���� �÷��̾��� ���
        {
            Debug.Log("�÷��̾ �����մϴ�!");
            // �÷��̾�� �������� �� �� �ִ� �Լ� ȣ��
        }
    }
}

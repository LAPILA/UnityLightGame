using UnityEngine;

public class MonsterAnimationPosition : MonoBehaviour
{
    private Vector3 originalPosition; // ������ ���� ��ġ
    private bool isPlayerInRange; // �÷��̾ ���� �ȿ� �ִ��� ���� Ȯ���ϴ� ����
    private float timeInRange; // �÷��̾ ���� �ȿ� �ӹ��� �ð�

    private Animator animator; // ������ �ִϸ�����

    private void Awake()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ��������
        originalPosition = transform.position; // ������ �ʱ� ��ġ ���
        isPlayerInRange = false; // �ʱ⿡�� �÷��̾ ���� �ȿ� ����
        timeInRange = 0f; // �ʱ⿡�� �ð��� 0���� ����
    }

    private void Update()
    {
        // �÷��̾ ���� �ȿ� �ְ�, �ð��� 1.5�� �̻��̸� ���� �ִϸ��̼� ����
        if (isPlayerInRange) {
            timeInRange += Time.deltaTime; // �ð��� ����

            if (timeInRange >= 1.5f) {
                animator.SetBool("isAttacking", true); // Attack �ִϸ��̼� ����
            }
        }
        else {
            timeInRange = 0f; // �÷��̾ ������ ����� �ð� �ʱ�ȭ
            animator.SetBool("isAttacking", false); // Attack �ִϸ��̼� ����
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // �÷��̾���� �浹 üũ
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isPlayerInRange = true; // �÷��̾ ���� �ȿ� �ִٰ� ǥ��
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾���� �浹 �� üũ
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            isPlayerInRange = false; // �÷��̾ ������ ����ٰ� ǥ��
        }
    }
}

using UnityEngine;

public class MonsterAnimationPosition : MonoBehaviour
{
    private Vector3 originalPosition; // ������ ���� ��ġ

    private Animator animator; // ������ �ִϸ�����

    private void Awake()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ��������
        originalPosition = transform.position; // ������ �ʱ� ��ġ ���
    }

    public void StartAttackAnimation()
    {
        // Attack �ִϸ��̼� ������ �� ��ġ ���
        originalPosition = transform.position;
        // �̰��� Attack �ִϸ��̼� ���� �ڵ� �߰�
        animator.SetBool("isAttacking", true); // Attack �ִϸ��̼� ����
    }

    public void EndAttackAnimation()
    {
        // Attack �ִϸ��̼��� ������ ���� ��ġ�� �ǵ���
        animator.SetBool("isAttacking", false); // Attack �ִϸ��̼� ����
        transform.position = originalPosition; // ���� ��ġ�� ���ư�
    }
}

using System.Collections;
using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    [SerializeField] CircleCollider2D Light_radius; // �÷��̾��� �� ���� �ݶ��̴�
    [SerializeField] BoxCollider2D Monster_box; // ������ �ݶ��̴�
    [SerializeField] BoxCollider2D AttackRangeCollider; // ���� ���� �ݶ��̴�

    private bool isInLight = false; // ���� ����Ǿ����� ����
    private bool isAttacking = false; // ���� ������ ����

    private Animator animator; // ������ �ִϸ�����

    private void Awake()
    {
        animator = GetComponent<Animator>(); // �ִϸ����� ��������
        AttackRangeCollider.enabled = false; // AttackRange Collider �ʱ⿡ ��Ȱ��ȭ
    }

    private void Update()
    {
        if (isAttacking) {
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = true; // ���� �����
            StartAttack(); // ���� ����
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = false; // ������ ���
            StopAttack(); // ���� ����
        }
    }

    private void StartAttack()
    {
        isAttacking = true; // ���� ���·� ����
        animator.SetBool("isAttacking", true); // �ִϸ��̼� ���� ����
    }

    private void StopAttack()
    {
        isAttacking = false; // ���� ����
        animator.SetBool("isAttacking", false); // �ִϸ��̼� ���� ����
    }

    private void GameOver()
    {
        Debug.Log("���� ����!"); // ���⿡ ���� ���� ������ �߰��ϼ���
        // ���� ���� ó�� ���� �ڵ带 �ۼ��ϼ���
    }
}

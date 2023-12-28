using System.Collections;
using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    [SerializeField] CircleCollider2D Light_radius; // �÷��̾��� �� ���� �ݶ��̴�
    [SerializeField] BoxCollider2D Monster_box; // ������ �ݶ��̴�
    [SerializeField] GameObject AttackRange; // ���� ������ ��Ÿ���� ���� ������Ʈ

    private bool isInLight = false; // ���� ����Ǿ����� ����
    private bool isAttacking = false; // ���� ������ ����
    private float lightExposureTime = 0f; // ���� ����� �ð�

    private Animator animator; // ������ �ִϸ�����
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (AttackRange != null) {
            AttackRange.SetActive(false); // ���� ���� ������Ʈ �ʱ⿡ ��Ȱ��ȭ
        }
    }

    private void Update()
    {
        if (isInLight) {
            lightExposureTime += Time.deltaTime;
            if (lightExposureTime >= 2f && !isAttacking) {
                StartAttack();
            }
        }
        else {
            lightExposureTime = 0f;
            if (isAttacking) {
                StopAttack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == Light_radius) {
            isInLight = false;
        }
    }

    private void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
        audioSource.Play();
        if (AttackRange != null) {
            AttackRange.SetActive(true); // ���� ���� ������Ʈ Ȱ��ȭ
        }
    }

    private void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        if (AttackRange != null) {
            AttackRange.SetActive(false); // ���� ���� ������Ʈ ��Ȱ��ȭ
        }
    }

}

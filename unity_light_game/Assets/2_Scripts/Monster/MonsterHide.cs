using System.Collections;
using UnityEngine;

public class MonsterHide : MonoBehaviour
{
    [SerializeField] CircleCollider2D Light_radius; // 플레이어의 빛 감지 콜라이더
    [SerializeField] BoxCollider2D Monster_box; // 몬스터의 콜라이더
    [SerializeField] GameObject AttackRange; // 공격 범위를 나타내는 게임 오브젝트

    private bool isInLight = false; // 빛에 노출되었는지 여부
    private bool isAttacking = false; // 공격 중인지 여부
    private float lightExposureTime = 0f; // 빛에 노출된 시간

    private Animator animator; // 몬스터의 애니메이터
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (AttackRange != null) {
            AttackRange.SetActive(false); // 공격 범위 오브젝트 초기에 비활성화
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
            AttackRange.SetActive(true); // 공격 범위 오브젝트 활성화
        }
    }

    private void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        if (AttackRange != null) {
            AttackRange.SetActive(false); // 공격 범위 오브젝트 비활성화
        }
    }

}

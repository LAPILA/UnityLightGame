using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target; // 따라다닐 타겟 오브젝트
    public float moveSpeed = 2f; // 이동 속도
    public float yOffset = 0.1f; // y축으로 이동할 값
    public float stopDelay = 0.01f; // 도착 후 정지할 시간

    private bool isMoving = false;
    private Vector3 targetPosition;
    private float stopTime = 0f;

    private void Update()
    {
        if (target != null) {
            if (!isMoving) {
                // 타겟 위치 계산
                targetPosition = target.position + Vector3.up * yOffset;
                isMoving = true;
            }

            if (isMoving) {
                // 타겟 방향과 거리 구하기
                Vector3 direction = targetPosition - transform.position;
                float distance = moveSpeed * Time.deltaTime;

                // 이동
                if (direction.magnitude > distance) {
                    transform.position += direction.normalized * distance;
                }
                else {
                    // 도착 후 일정 시간 정지
                    if (stopTime < stopDelay) {
                        stopTime += Time.deltaTime;
                    }
                    else {
                        isMoving = false;
                        stopTime = 0f;
                    }
                }
            }
        }
    }
}

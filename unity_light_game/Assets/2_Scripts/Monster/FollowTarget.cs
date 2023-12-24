using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target; // ����ٴ� Ÿ�� ������Ʈ
    public float moveSpeed = 2f; // �̵� �ӵ�
    public float yOffset = 0.1f; // y������ �̵��� ��
    public float stopDelay = 0.01f; // ���� �� ������ �ð�

    private bool isMoving = false;
    private Vector3 targetPosition;
    private float stopTime = 0f;

    private void Update()
    {
        if (target != null) {
            if (!isMoving) {
                // Ÿ�� ��ġ ���
                targetPosition = target.position + Vector3.up * yOffset;
                isMoving = true;
            }

            if (isMoving) {
                // Ÿ�� ����� �Ÿ� ���ϱ�
                Vector3 direction = targetPosition - transform.position;
                float distance = moveSpeed * Time.deltaTime;

                // �̵�
                if (direction.magnitude > distance) {
                    transform.position += direction.normalized * distance;
                }
                else {
                    // ���� �� ���� �ð� ����
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

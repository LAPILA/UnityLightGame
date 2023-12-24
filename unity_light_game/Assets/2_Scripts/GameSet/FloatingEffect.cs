using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float floatStrength = 0.5f; // ���ٴϴ� ����
    public float floatSpeed = 1f; // ���ٴϴ� �ӵ�

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // ���Ʒ��� ���ٴϴ� ������ �Լ� ȣ��
        Float();
    }

    private void Float()
    {
        // ���ٴϴ� ������ ���
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

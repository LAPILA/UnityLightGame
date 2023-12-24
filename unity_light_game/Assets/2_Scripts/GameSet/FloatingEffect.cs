using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    public float floatStrength = 0.5f; // 떠다니는 세기
    public float floatSpeed = 1f; // 떠다니는 속도

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // 위아래로 떠다니는 움직임 함수 호출
        Float();
    }

    private void Float()
    {
        // 떠다니는 움직임 계산
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

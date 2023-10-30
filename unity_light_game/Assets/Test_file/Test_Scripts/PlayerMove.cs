using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 스크롤바를 위해 필요

public class PlayerMove : MonoBehaviour
{
    // 플레이어 이동에 필요한 변수들
    public float Speed;
    public float runMultiplier = 1.5f; // 달릴 때의 속도 배율
    private Rigidbody2D rigid;
    private Animator animator;
    private float h;
    private float v;
    private Vector3 dirVec;
    private GameObject scanObject;

    // 횃불 개수에 관련된 변수들
    public TextMeshProUGUI torchCountText;
    private int totalTorches = 8;
    private int litTorches = 0;
    private TorchController detectedTorch;

    // 스테미나 관련 변수들
    public Scrollbar staminaScrollbar;
    private float stamina = 5f;
    private float currentStamina;
    private float staminaRecoveryDelay = 1f; // 스테미나가 다 떨어진 후 회복까지의 시간
    private float quickRecoveryRate = 0.2f; // 빠른 회복율
    private bool isRunning = false;
    private float lastTimeRunning;

    void Awake()
    {
        // 초기화
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = stamina; // 스테미나 초기화
        UpdateTorchCountText();
        UpdateStaminaUI(currentStamina, stamina);
    }

    private void Update()
    {
        // 사용자 입력 및 애니메이션
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirVec = new Vector3(h, v, 0).normalized;

        // 달리기와 스테미나 소모
        isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
        if (isRunning) {
            dirVec *= runMultiplier;
            UseStamina(Time.deltaTime);
            lastTimeRunning = Time.time;
        }
        else if (Time.time > lastTimeRunning + staminaRecoveryDelay) {
            RecoverStamina(quickRecoveryRate * Time.deltaTime);
        }

        UpdateAnimation();
        FlipCharacter();

        // 횃불과 상호작용
        if (Input.GetKeyDown(KeyCode.Space) && detectedTorch != null) {
            detectedTorch.ActivateTorch();
        }
    }

    private void FixedUpdate()
    {
        // 캐릭터 이동
        rigid.velocity = dirVec * Speed;
        DetectObjects();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 횃불과의 상호작용
        if (collision.gameObject.layer == LayerMask.NameToLayer("object")) {
            detectedTorch = collision.gameObject.GetComponent<TorchController>();
            if (detectedTorch != null && !detectedTorch.IsInteracted()) {
                detectedTorch.ActivateTorch();
                detectedTorch.SetInteracted(true);
                litTorches++;
                UpdateTorchCountText();
                if (PlayerVision.Instance != null) {
                    PlayerVision.Instance.LightTorch();
                }
                if (litTorches >= totalTorches) GameClear();
            }
        }
    }

    // 스테미나를 사용하는 함수
    void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        UpdateStaminaUI(currentStamina, stamina); // UI 업데이트
    }

    // 스테미나를 회복하는 함수
    void RecoverStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        UpdateStaminaUI(currentStamina, stamina); // UI 업데이트
    }

    // 스테미나 UI를 업데이트하는 함수
    void UpdateStaminaUI(float currentStamina, float maxStamina)
    {
        float normalizedStamina = currentStamina / maxStamina;
        staminaScrollbar.size = normalizedStamina;  
    }

    // 남은 횃불 수를 업데이트하는 함수
    void UpdateTorchCountText()
    {
        torchCountText.text = $"남은 Torch: {totalTorches - litTorches}";
    }

    // 게임 클리어시 실행되는 함수
    void GameClear()
    {
        Debug.Log("게임 클리어!");
        SceneManager.LoadScene("GameClear");
    }

    // 애니메이션을 업데이트하는 함수
    void UpdateAnimation()
    {
        animator.SetBool("isWALK", Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
    }

    // 캐릭터의 방향을 바꾸는 함수
    void FlipCharacter()
    {
        if (h > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    // 오브젝트를 탐지하는 함수
    void DetectObjects()
    {
        Debug.DrawRay(rigid.position, dirVec * 0.2f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.2f, LayerMask.GetMask("object"));

        if (rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
            detectedTorch = scanObject.GetComponent<TorchController>();
        }
        else {
            scanObject = null;
            detectedTorch = null;
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��ũ�ѹٸ� ���� �ʿ�

public class PlayerMove : MonoBehaviour
{
    // �÷��̾� �̵��� �ʿ��� ������
    public float Speed;
    public float runMultiplier = 1.5f; // �޸� ���� �ӵ� ����
    private Rigidbody2D rigid;
    private Animator animator;
    private float h;
    private float v;
    private Vector3 dirVec;
    private GameObject scanObject;

    // ȶ�� ������ ���õ� ������
    public TextMeshProUGUI torchCountText;
    private int totalTorches = 8;
    private int litTorches = 0;
    private TorchController detectedTorch;

    // ���׹̳� ���� ������
    public Scrollbar staminaScrollbar;
    private float stamina = 5f;
    private float currentStamina;
    private float staminaRecoveryDelay = 1f; // ���׹̳��� �� ������ �� ȸ�������� �ð�
    private float quickRecoveryRate = 0.2f; // ���� ȸ����
    private bool isRunning = false;
    private float lastTimeRunning;

    void Awake()
    {
        // �ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = stamina; // ���׹̳� �ʱ�ȭ
        UpdateTorchCountText();
        UpdateStaminaUI(currentStamina, stamina);
    }

    private void Update()
    {
        // ����� �Է� �� �ִϸ��̼�
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirVec = new Vector3(h, v, 0).normalized;

        // �޸���� ���׹̳� �Ҹ�
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

        // ȶ�Ұ� ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.Space) && detectedTorch != null) {
            detectedTorch.ActivateTorch();
        }
    }

    private void FixedUpdate()
    {
        // ĳ���� �̵�
        rigid.velocity = dirVec * Speed;
        DetectObjects();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ȶ�Ұ��� ��ȣ�ۿ�
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

    // ���׹̳��� ����ϴ� �Լ�
    void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        UpdateStaminaUI(currentStamina, stamina); // UI ������Ʈ
    }

    // ���׹̳��� ȸ���ϴ� �Լ�
    void RecoverStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        UpdateStaminaUI(currentStamina, stamina); // UI ������Ʈ
    }

    // ���׹̳� UI�� ������Ʈ�ϴ� �Լ�
    void UpdateStaminaUI(float currentStamina, float maxStamina)
    {
        float normalizedStamina = currentStamina / maxStamina;
        staminaScrollbar.size = normalizedStamina;  
    }

    // ���� ȶ�� ���� ������Ʈ�ϴ� �Լ�
    void UpdateTorchCountText()
    {
        torchCountText.text = $"���� Torch: {totalTorches - litTorches}";
    }

    // ���� Ŭ����� ����Ǵ� �Լ�
    void GameClear()
    {
        Debug.Log("���� Ŭ����!");
        SceneManager.LoadScene("GameClear");
    }

    // �ִϸ��̼��� ������Ʈ�ϴ� �Լ�
    void UpdateAnimation()
    {
        animator.SetBool("isWALK", Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f);
    }

    // ĳ������ ������ �ٲٴ� �Լ�
    void FlipCharacter()
    {
        if (h > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    // ������Ʈ�� Ž���ϴ� �Լ�
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

using System.Collections;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��ũ�ѹٸ� ���� �ʿ�

public class PlayerMove : MonoBehaviour
{
    // �÷��̾� �̵��� �ʿ��� ������
    public float Speed;
    public float runMultiplier = 2f; // �޸� ���� �ӵ� ����
    private Rigidbody2D rigid;
    private Animator animator;
    private float h;
    private float v;
    private Vector3 dirVec;
    private GameObject scanObject;
    private Vector3 lastMoveDir;

    // ȶ�� ������ ���õ� ������
    public TextMeshProUGUI torchCountText;
    private int totalTorches = 9;
    private int litTorches = 0;
    private TorchController detectedTorch;

    // ���׹̳� ���� ������
    public Scrollbar staminaScrollbar;
    private float stamina = 5f;
    private float currentStamina;
    private float staminaRecoveryDelay = 1f; // ���׹̳��� �� ������ �� ȸ�������� �ð�
    private float staminaBurnRate = 1f; // �޸��� �� ���׹̳� �Ҹ���
    private float staminaRecoveryRate = 0.2f; // ���׹̳� ȸ����
    private bool isRunning = false;
    private float lastTimeRunning;
    //��ȣ�ۿ� ���� object
    private MonsterChest dectectedChest;


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

        UpdateRunningStatus();
        UpdateAnimation();
        FlipCharacter();
        if (h != 0 || v != 0) // �������� �ִ� ��쿡�� dirVec�� ������Ʈ
    {
            dirVec = new Vector3(h, v, 0).normalized;
            lastMoveDir = dirVec; // �÷��̾ �����̴� ���ȿ��� �� ������ ��� ����
        }
        DetectObjects();

        // ȶ�Ұ� ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.Space) && detectedTorch != null) {
            InteractWithTorch();
        }
        //chestMob ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.Space) && dectectedChest != null)
        {
            Debug.Log("�浹�ν�");
            InteractWithChestMob();
        }

    }

    private void FixedUpdate()
    {
        // ĳ���� �̵�
        rigid.velocity = dirVec * (isRunning ? Speed * runMultiplier : Speed);
        DetectObjects();
    }
    void UpdateRunningStatus()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0) {
            StartRunning();
        }
        else {
            StopRunning();
        }

        // ���׹̳� ȸ��
        if (!isRunning && Time.time > lastTimeRunning + staminaRecoveryDelay) {
            RecoverStamina(staminaRecoveryRate * Time.deltaTime);
        }
    }
    void StartRunning()
    {
        isRunning = true;
        UseStamina(staminaBurnRate * Time.deltaTime);
        lastTimeRunning = Time.time; // ���׹̳� ȸ�� ���� �ð��� ���� ������ �޸� �ð� ����
    }

    // �޸��� �ߴ��ϴ� �Լ�
    void StopRunning()
    {
        isRunning = false;
    }

    // ���׹̳��� ����ϴ� �Լ�
    void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        UpdateStaminaUI(currentStamina, stamina); // UI ������Ʈ
    }

    // ���׹̳��� ȸ���ϴ� �Լ�
    public void RecoverStamina(float amount)
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

        if (v > 0) // Moving up
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFront", false);
        }
        else if (v < 0) // Moving down
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFront", true);
        }
        else // Not moving vertically
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFront", false);
        }
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
        Vector3 scanDir = dirVec == Vector3.zero ? lastMoveDir : dirVec;
        Debug.DrawRay(rigid.position, scanDir * 0.2f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, scanDir, 0.2f, LayerMask.GetMask("object"));

        if (rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
            Debug.Log(scanObject);
            // ���� ���� ��ü Ž�� 
            MonsterChest chMob = scanObject.GetComponent<MonsterChest>();
            if (chMob != null && !chMob.IsInteracted())
            {
                dectectedChest = chMob;
            }
            // ��ġ ��ü Ž�� ����
            TorchController hitTorch = scanObject.GetComponent<TorchController>();
            if (hitTorch != null && !hitTorch.IsInteracted()) {
                detectedTorch = hitTorch;
            }
        }
        else {
            detectedTorch = null;
            dectectedChest = null;
        }
    }

    void InteractWithTorch()
    {
        if (detectedTorch != null && !detectedTorch.IsInteracted()) {
            detectedTorch.ActivateTorch();
            detectedTorch.SetInteracted(true);
            litTorches++;
            UpdateTorchCountText();
            if (PlayerVision.Instance != null) {
                PlayerVision.Instance.LightTorch();
            }
            if (litTorches >= totalTorches) {
                GameClear();
            }
        }
    }
    void InteractWithChestMob()
    {
        if (dectectedChest != null && !dectectedChest.IsInteracted())
        {
            dectectedChest.ActivateChestMob();
        }
    }

}

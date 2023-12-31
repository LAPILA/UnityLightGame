using System.Collections;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 스크롤바를 위해 필요

public class PlayerMove : MonoBehaviour
{
    public static bool fisrtGame = false;
    // 플레이어 이동에 필요한 변수들
    public float Speed;
    public float runMultiplier = 1.5f; // 달릴 때의 속도 배율
    private Rigidbody2D rigid;
    private Animator animator;
    private float h;
    private float v;
    private Vector3 dirVec;
    private GameObject scanObject;
    GameObject canvasObject;
    private Vector3 lastMoveDir;
    AudioSource audioSource;

    // 횃불 개수에 관련된 변수들
    public TextMeshProUGUI torchCountText;
    int totalTorches = 18;
    public static int litTorches = 0;
    private TorchController detectedTorch;

    // 스테미나 관련 변수들
    public Scrollbar staminaScrollbar;
    private float stamina = 5f;
    private float currentStamina;
    private float staminaRecoveryDelay = 1f; // 스테미나가 다 떨어진 후 회복까지의 시간
    private float staminaBurnRate = 1f; // 달리기 시 스테미나 소모율
    private float staminaRecoveryRate = 0.2f; // 스테미나 회복율
    private bool isRunning = false;
    private float lastTimeRunning;
    //상호작용 관련 object
    private MonsterChest dectectedChest;
    //게임오버, 부활 관련 조정 오브젝트 
    public Game game;
    private bool isAudioPlaying = false;
    private void Start()
    {
        isAudioPlaying = false;
        audioSource = GetComponent<AudioSource>();
        if (game == null) {
            game = FindObjectOfType<Game>();
        }
    }
    void Awake()
    {
        
        canvasObject = canvasObject = GameObject.Find("PlayerUI");
        // 초기화
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentStamina = stamina; // 스테미나 초기화
        UpdateTorchCountText();
        UpdateStaminaUI(currentStamina, stamina);
    }

    private void Update()
    {
        UpdateTorchCountText();
        // 사용자 입력 및 애니메이션
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirVec = new Vector3(h, v, 0).normalized;
        bool isMoving = h != 0 || v != 0;

        UpdateRunningStatus();
        UpdateAnimation();
        FlipCharacter();
        ControlAudioPlayback(isMoving);

        if (isMoving) {
            dirVec = new Vector3(h, v, 0).normalized;
            lastMoveDir = dirVec;
        }
        DetectObjects();

        // 횃불과 상호작용
        if (Input.GetKeyDown(KeyCode.Space) && detectedTorch != null) {
            InteractWithTorch();
        }
        //chestMob 상호작용
        if (Input.GetKeyDown(KeyCode.Space) && dectectedChest != null)
        {
            Debug.Log("충돌인식");
            InteractWithChestMob();
        }
        if (Input.GetKeyDown(KeyCode.V)&&game.Gameover) {
            game.ReGame();
        }

        Ending();
    }


    private void FixedUpdate()
    {
        // 캐릭터 이동
        rigid.velocity = dirVec * (isRunning ? Speed * runMultiplier : Speed);
        DetectObjects();
    }

    void ControlAudioPlayback(bool isMoving)
    {
        if (isMoving) {
            if (!isAudioPlaying) {
                audioSource.Play();
                isAudioPlaying = true;
            }
        }
        else {
            if (isAudioPlaying) {
                audioSource.Stop();
                isAudioPlaying = false;
            }
        }
    }

    void UpdateRunningStatus()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0) {
            audioSource.pitch = 1.3f;
            StartRunning();
        }
        else {
            audioSource.pitch = 0.8f;
            StopRunning();
        }

        // 스테미나 회복
        if (!isRunning && Time.time > lastTimeRunning + staminaRecoveryDelay) {
            RecoverStamina(staminaRecoveryRate * Time.deltaTime);
        }
    }
    void StartRunning()
    {
        isRunning = true;
        UseStamina(staminaBurnRate * Time.deltaTime);
        lastTimeRunning = Time.time; // 스테미나 회복 지연 시간을 위해 마지막 달린 시간 갱신
    }

    // 달리기 중단하는 함수
    void StopRunning()
    {
        isRunning = false;
    }

    // 스테미나를 사용하는 함수
    void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        UpdateStaminaUI(currentStamina, stamina); // UI 업데이트
    }

    // 스테미나를 회복하는 함수
    public void RecoverStamina(float amount)
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


    // 애니메이션을 업데이트하는 함수
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

    // 캐릭터의 방향을 바꾸는 함수
    void FlipCharacter()
    {
        if (h > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (h < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    // 오브젝트를 탐지하는 함수
    void DetectObjects()
    {
        Vector3 scanDir = dirVec == Vector3.zero ? lastMoveDir : dirVec;
        Debug.DrawRay(rigid.position, scanDir * 0.2f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, scanDir, 0.2f, LayerMask.GetMask("object"));

        if (rayHit.collider != null) {
            scanObject = rayHit.collider.gameObject;
            //Debug.Log(scanObject);
            // 상자 몬스터 객체 탐지 
            MonsterChest chMob = scanObject.GetComponent<MonsterChest>();
            if (chMob != null && !chMob.IsInteracted())
            {
                dectectedChest = chMob;
            }
            // 토치 객체 탐지 로직
            TorchController hitTorch = scanObject.GetComponent<TorchController>();
            if (hitTorch != null && !hitTorch.IsInteracted()) {
                detectedTorch = hitTorch;
                game.SetReturn(new Vector3(hitTorch.transform.position.x, hitTorch.transform.position.y, hitTorch.transform.position.z));
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
            
        }
    }


    void Ending()
    {
        if (litTorches >= totalTorches) {
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(2f); 

        SceneManager.LoadScene("GameClear"); // 다음 씬으로 이동
    }

    void InteractWithChestMob()
    {
        if (dectectedChest != null && !dectectedChest.IsInteracted())
        {
            dectectedChest.ActivateChestMob();
        }
    }
    
}

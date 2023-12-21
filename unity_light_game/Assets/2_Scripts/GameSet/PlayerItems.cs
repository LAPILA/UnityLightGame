using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerItems : MonoBehaviour
{
    private static PlayerItems instance;

    public float glowBagDuration = 10f; // Inspector에서 수정 가능하도록 public으로 변경
    public float glowBagCooldown = 10f; // GlowBag 쿨타임 시간
    private float glowBagCooldownTime = 0f; // 쿨타임을 체크할 변수

    public GameObject beaconPrefab; // Inspector에서 Beacon 프리팹을 할당할 수 있도록 public 변수로 선언

    // PlayerMove와 PlayerVision 컴포넌트에 대한 public 참조
    public PlayerMove playerMove;
    public PlayerVision playerVision;

    // 아이템 인벤토리와 관련된 변수
    private Dictionary<string, int> items = new Dictionary<string, int>(); // 아이템과 수량을 저장할 딕셔너리
    private Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();

    private string currentItemKey; // 현재 선택된 아이템 키
    private int currentItemIndex = 0; // 현재 선택된 아이템의 인덱스
    public Sprite breadSprite;
    public Sprite glowBagSprite;
    public Sprite beaconSprite;
    public Sprite nullSprtie;

    // UI 관련 변수
    public Image itemImageUI; // 아이템 이미지를 표시할 Image UI
    public TextMeshProUGUI itemCountUI; // 아이템 갯수를 표시할 Text UI


    private void Start()
    {
        // 컴포넌트가 할당되지 않았을 경우, GameObject에서 자동으로 찾아 할당
        if (!playerMove) playerMove = GetComponent<PlayerMove>();
        if (!playerVision) playerVision = GetComponent<PlayerVision>();
        AddItemSprite("Bread", breadSprite);
        AddItemSprite("GlowBag", glowBagSprite);
        AddItemSprite("Beacon", beaconSprite);

        InitializeInventory(); // 초기 아이템 설정
        UpdateUI(); // UI 업데이트
    }

    private void InitializeInventory()
    {
        // 시작 시 일부 아이템을 인벤토리에 추가
        AddItem("Bread", 5); 
        AddItem("GlowBag", 2);
        AddItem("Beacon", 3);

        if (items.Count > 0) {
            currentItemIndex = 0;
            currentItemKey = new List<string>(items.Keys)[currentItemIndex];
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            SwitchItem();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            UseCurrentItem();
        }
        
        if (Input.GetKeyDown(KeyCode.Z)) {
            AddItem("Beacon", 3);
        }
    }
    public void AddItemSprite(string itemName, Sprite sprite)
    {
        // 아이템 스프라이트 추가
        if (!itemSprites.ContainsKey(itemName)) {
            itemSprites[itemName] = sprite;
        }
    }
    private void SwitchItem()
    {
        // 존재하는 아이템만을 위한 리스트 생성
        List<string> availableItems = new List<string>();
        foreach (var item in items) {
            if (item.Value > 0) {
                availableItems.Add(item.Key);
            }
        }

        // 존재하는 아이템이 없으면 함수 종료
        if (availableItems.Count == 0) return;

        // 다음 아이템으로 전환
        currentItemIndex = (currentItemIndex + 1) % availableItems.Count;
        currentItemKey = availableItems[currentItemIndex];
        UpdateUI(); // UI 업데이트
    }


    private void UseCurrentItem()
    {
        if (!items.ContainsKey(currentItemKey) || items[currentItemKey] <= 0) return;
        
        switch (currentItemKey) {
            case "Bread":
                UseBread();
                items[currentItemKey]--;
                break;
            case "GlowBag":
                if (Time.time > glowBagCooldownTime) {
                    StartCoroutine(UseGlowBag());
                    glowBagCooldownTime = Time.time + glowBagCooldown; // 쿨타임 설정
                    items[currentItemKey]--;
                }
                break;
            case "Beacon":
                PlaceBeacon();
                items[currentItemKey]--;
                break;
        }
        if (items[currentItemKey] <= 0) {
            SwitchItem(); // 다음 아이템으로 전환
        }
        UpdateUI();
    }


    public void AddItem(string itemName, int count)
    {
        // 아이템 추가 메서드
        if (!items.ContainsKey(itemName))
            items[itemName] = 0;

        items[itemName] += count; // 수량 추가
        UpdateUI(); // UI 업데이트
    }

    private void UpdateUI()
    {
        // currentItemKey가 null이거나, 아이템이 없거나, 아이템 수량이 0 이하면 UI를 초기화
        if (string.IsNullOrEmpty(currentItemKey) || !items.ContainsKey(currentItemKey) || items[currentItemKey] <= 0) {
            itemImageUI.sprite = nullSprtie;
            itemCountUI.text = "";
        }
        else {
            itemImageUI.sprite = itemSprites[currentItemKey];
            itemCountUI.text = items[currentItemKey].ToString();
        }
    }

    private void UseBread()
    {
        // 빵을 사용할 때의 효과를 실행
        playerMove.RecoverStamina(10f);
    }

    private IEnumerator UseGlowBag()
    {
        //GlowBag 실행 도중 TorchRight 감소 영향 안가게하는  함수 필수
        // GlowBag 효과 실행
        float originalOuterRadius = playerVision.playerLight.pointLightOuterRadius;
        playerVision.playerLight.pointLightOuterRadius *= 2f; // 시야 범위 2배 증가

        yield return new WaitForSeconds(glowBagDuration); // 지속 시간 동안 기다림

        playerVision.playerLight.pointLightOuterRadius = originalOuterRadius; // 시야 범위 복구
    }

    private void PlaceBeacon()
    {
        // Beacon 위치를 플레이어 현재 위치에 배치
        Instantiate(beaconPrefab, transform.position, Quaternion.identity);
    }
}

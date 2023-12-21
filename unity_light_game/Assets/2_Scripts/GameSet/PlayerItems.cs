using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerItems : MonoBehaviour
{
    private static PlayerItems instance;

    public float glowBagDuration = 10f; // Inspector���� ���� �����ϵ��� public���� ����
    public float glowBagCooldown = 10f; // GlowBag ��Ÿ�� �ð�
    private float glowBagCooldownTime = 0f; // ��Ÿ���� üũ�� ����

    public GameObject beaconPrefab; // Inspector���� Beacon �������� �Ҵ��� �� �ֵ��� public ������ ����

    // PlayerMove�� PlayerVision ������Ʈ�� ���� public ����
    public PlayerMove playerMove;
    public PlayerVision playerVision;

    // ������ �κ��丮�� ���õ� ����
    private Dictionary<string, int> items = new Dictionary<string, int>(); // �����۰� ������ ������ ��ųʸ�
    private Dictionary<string, Sprite> itemSprites = new Dictionary<string, Sprite>();

    private string currentItemKey; // ���� ���õ� ������ Ű
    private int currentItemIndex = 0; // ���� ���õ� �������� �ε���
    public Sprite breadSprite;
    public Sprite glowBagSprite;
    public Sprite beaconSprite;
    public Sprite nullSprtie;

    // UI ���� ����
    public Image itemImageUI; // ������ �̹����� ǥ���� Image UI
    public TextMeshProUGUI itemCountUI; // ������ ������ ǥ���� Text UI


    private void Start()
    {
        // ������Ʈ�� �Ҵ���� �ʾ��� ���, GameObject���� �ڵ����� ã�� �Ҵ�
        if (!playerMove) playerMove = GetComponent<PlayerMove>();
        if (!playerVision) playerVision = GetComponent<PlayerVision>();
        AddItemSprite("Bread", breadSprite);
        AddItemSprite("GlowBag", glowBagSprite);
        AddItemSprite("Beacon", beaconSprite);

        InitializeInventory(); // �ʱ� ������ ����
        UpdateUI(); // UI ������Ʈ
    }

    private void InitializeInventory()
    {
        // ���� �� �Ϻ� �������� �κ��丮�� �߰�
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
        // ������ ��������Ʈ �߰�
        if (!itemSprites.ContainsKey(itemName)) {
            itemSprites[itemName] = sprite;
        }
    }
    private void SwitchItem()
    {
        // �����ϴ� �����۸��� ���� ����Ʈ ����
        List<string> availableItems = new List<string>();
        foreach (var item in items) {
            if (item.Value > 0) {
                availableItems.Add(item.Key);
            }
        }

        // �����ϴ� �������� ������ �Լ� ����
        if (availableItems.Count == 0) return;

        // ���� ���������� ��ȯ
        currentItemIndex = (currentItemIndex + 1) % availableItems.Count;
        currentItemKey = availableItems[currentItemIndex];
        UpdateUI(); // UI ������Ʈ
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
                    glowBagCooldownTime = Time.time + glowBagCooldown; // ��Ÿ�� ����
                    items[currentItemKey]--;
                }
                break;
            case "Beacon":
                PlaceBeacon();
                items[currentItemKey]--;
                break;
        }
        if (items[currentItemKey] <= 0) {
            SwitchItem(); // ���� ���������� ��ȯ
        }
        UpdateUI();
    }


    public void AddItem(string itemName, int count)
    {
        // ������ �߰� �޼���
        if (!items.ContainsKey(itemName))
            items[itemName] = 0;

        items[itemName] += count; // ���� �߰�
        UpdateUI(); // UI ������Ʈ
    }

    private void UpdateUI()
    {
        // currentItemKey�� null�̰ų�, �������� ���ų�, ������ ������ 0 ���ϸ� UI�� �ʱ�ȭ
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
        // ���� ����� ���� ȿ���� ����
        playerMove.RecoverStamina(10f);
    }

    private IEnumerator UseGlowBag()
    {
        //GlowBag ���� ���� TorchRight ���� ���� �Ȱ����ϴ�  �Լ� �ʼ�
        // GlowBag ȿ�� ����
        float originalOuterRadius = playerVision.playerLight.pointLightOuterRadius;
        playerVision.playerLight.pointLightOuterRadius *= 2f; // �þ� ���� 2�� ����

        yield return new WaitForSeconds(glowBagDuration); // ���� �ð� ���� ��ٸ�

        playerVision.playerLight.pointLightOuterRadius = originalOuterRadius; // �þ� ���� ����
    }

    private void PlaceBeacon()
    {
        // Beacon ��ġ�� �÷��̾� ���� ��ġ�� ��ġ
        Instantiate(beaconPrefab, transform.position, Quaternion.identity);
    }
}

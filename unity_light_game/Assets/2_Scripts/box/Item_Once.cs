using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Once : MonoBehaviour
{
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject canvas;

    private bool imagesShown = false;
    private bool isPaused = false;
    private bool canInteract = false;
    private bool itemOn = true;

    private SpriteRenderer spriteRenderer;
    private PlayerItems playerItems;
    public Sprite openedBoxSprite;

    private void Start()
    {
        InitializeComponents();
        DeactivateImages();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canInteract && itemOn) {
            AddRandomItem();
        }

        HandlePause();
        HandleImageDisplay();
    }

    void InitializeComponents()
    {
        playerItems = GameObject.Find("player").GetComponent<PlayerItems>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null) {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Debug.Log("¿À·ù!!!!!!!!!!!!!!!");
        }
    }

    void DeactivateImages()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
    }

    void AddRandomItem()
    {
        playerItems.AddItem("Bread", 1);
        playerItems.AddItem("GlowBag", 1);
        playerItems.AddItem("Beacon", 1);

        if (openedBoxSprite != null) {
            spriteRenderer.sprite = openedBoxSprite;
        }

        itemOn = false;
        ShowImagesAndPauseGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canInteract = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canInteract = false;
    }

    void ShowImagesAndPauseGame()
    {
        image1.SetActive(true);
        imagesShown = true;
        isPaused = true;

        Time.timeScale = 0f;
    }

    void HandlePause()
    {
        if (isPaused) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }

    void HandleImageDisplay()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            canvas.SetActive(false);
            imagesShown = false;
            isPaused = false;
        }
        if (imagesShown && Input.GetKeyDown(KeyCode.Space)) {
            if (image1.activeSelf) {
                image1.SetActive(false);
                image2.SetActive(true);
            }
            else if (image2.activeSelf) {
                image2.SetActive(false);
                image3.SetActive(true);
            }
            else if (image3.activeSelf) {
                image3.SetActive(false);
                image1.SetActive(true);
            }
            else {
                canvas.SetActive(false);
                imagesShown = false;
                isPaused = false;
            }
        }
    }

    public void OnBoxClick()
    {
        image1.SetActive(true);
        imagesShown = true;
        isPaused = true;
    }
}

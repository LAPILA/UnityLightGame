using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_small : MonoBehaviour
{
    bool canInteract = false;
    bool ItemOn = true;
    SpriteRenderer spriteRenderer;
    PlayerItems playerItems;
    public Sprite openedBoxSprite;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerItems = GameObject.Find("player").GetComponent<PlayerItems>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Debug.Log("¿À·ù!!!!!!!!!!!!!!!");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&canInteract &&ItemOn) {
            AddRandomItem();
            audioSource.Play();
        }
    }

    void AddRandomItem()
    {
        int randomNumber1 = Random.Range(1, 2);
        int randomNumber2 = Random.Range(0, 2);
        int randomNumber3 = Random.Range(0, 2);

        playerItems.AddItem("Bread", randomNumber1);
        playerItems.AddItem("GlowBag", randomNumber2);
        playerItems.AddItem("Beacon", randomNumber3);

        if (openedBoxSprite != null) {
            spriteRenderer.sprite = openedBoxSprite;
        }

        ItemOn = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        canInteract = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canInteract = false;
    }
}

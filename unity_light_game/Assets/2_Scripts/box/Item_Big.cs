using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Big : MonoBehaviour
{
    bool canInteract = false;
    bool ItemOn = true;
    SpriteRenderer spriteRenderer;
    PlayerItems playerItems;
    public Sprite openedBoxSprite;
    private void Start()
    {
        playerItems = GameObject.Find("player").GetComponent<PlayerItems>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Debug.Log("����!!!!!!!!!!!!!!!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canInteract && ItemOn) {
            AddRandomItem();
        }
    }

    void AddRandomItem()
    {
        int randomNumber1 = Random.Range(1, 5);
        int randomNumber2 = Random.Range(1, 5);
        int randomNumber3 = Random.Range(1, 5);

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
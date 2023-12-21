using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Once : MonoBehaviour
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
            Debug.Log("¿À·ù!!!!!!!!!!!!!!!");
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

        playerItems.AddItem("Bread", 1);
        playerItems.AddItem("GlowBag", 1);
        playerItems.AddItem("Beacon", 1);

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

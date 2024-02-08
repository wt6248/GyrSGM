using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemPrefabScript : MonoBehaviour
{
    public SpriteAtlas itemSpriteAtlas;
    public SpriteRenderer itemSpriteRenderer;
    public PlayerController playerController;
    public int itemType;
    // Start is called before the first frame update
    void Start()
    {
        itemSpriteAtlas = Resources.Load<SpriteAtlas>("Sprites/ItemSpriteAtlas");
        itemSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        
        SetItemType();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemType(int givenItemType = 0)
    {
        itemType = givenItemType;
        itemSpriteRenderer.sprite = itemSpriteAtlas.GetSprite("item_" + itemType.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //주인공과 충돌했을 때 주인공의 함수 호출. 
        if(other.gameObject.name == "main character" && playerController != null)
        {
            if (itemType == 0 || itemType == 1)
            {
                playerController.SetAttackDamage();
            }
            else if(itemType == 2 || itemType == 3)
            {
                playerController.SetSpeed();
            }
            else // item 4, 5 (체력키트, 알약)
            {
                playerController.SetHealthPoint();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class ItemPrefabScript : MonoBehaviour
{
    // init stat
    public SpriteAtlas itemSpriteAtlas;
    public SpriteRenderer itemSpriteRenderer;
    public PlayerController playerController;
    public ItemManage itemManage;
    public int _itemType;

    void Start()
    {
        //init
        itemSpriteAtlas = Resources.Load<SpriteAtlas>("Sprites/ItemSpriteAtlas");
        itemSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        itemManage = FindObjectOfType<ItemManage>();

        //function for retrieving sprites of random item types
        SetItemType();        
    }

    public void SetItemType()
    {
        //retrieve sprites of random item types
        _itemType = Random.Range(0, 5);
        if (_itemType == 0 || _itemType == 1)
        {
           _itemType = 0;
        }
        else if(_itemType == 2 || _itemType == 3)
        {
            _itemType = 2;            
        }
        else // item 4 (체력키트)
        {
            _itemType = 4;
        }
        itemSpriteRenderer.sprite = itemSpriteAtlas.GetSprite("item_" + _itemType.ToString());      
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //Debug.Log("collide with " + other.gameObject.name.ToString());
        //주인공과 충돌했을 때 주인공의 함수 호출. 
        if(other.gameObject.name == "Main Character" && playerController != null)
        {
            if (_itemType == 0 || _itemType == 1)
            {
                //Debug.Log("damage");
                playerController.SetAttackDamage();
            }
            else if(_itemType == 2 || _itemType == 3)
            {
                //Debug.Log("attack speed");
                playerController.SetAttackSpeed();
            }
            else // item 4 (체력키트)
            {
                //Debug.Log("Health");
                playerController.SetHealthPoint();
            }
            DestroyItem();
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
        if (itemManage != null)
        {
            itemManage.DestroyItem(); // 아이템 개수 줄이기
        }
    }
}


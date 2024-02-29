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
        _itemType = Random.Range(0, 3);
        switch(_itemType)
        {
            case 0: //damage
                itemSpriteRenderer.sprite = itemSpriteAtlas.GetSprite("D_Img");
                break;
            case 1: //attackspeed
                itemSpriteRenderer.sprite = itemSpriteAtlas.GetSprite("SP_Img");
                break;
            default:    //health
                itemSpriteRenderer.sprite = itemSpriteAtlas.GetSprite("HP_Img");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //주인공과 충돌했을 때 주인공의 함수 호출.
        if (other.gameObject.name == "Main Character" && playerController != null)
        {
            // envokePlayerEnhance();
            EnvokePlayerEnhance();
        }
    }


    private void EnvokePlayerEnhance()
    {
        switch(_itemType)
        {
            case 0: //damage
                playerController.SetAttackDamage();
                break;
            case 1: //attackspeed
                playerController.SetAttackSpeed();
                break;
            default:    //health
                playerController.SetHealthPoint();
                break;
        }
        DestroyItem();
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


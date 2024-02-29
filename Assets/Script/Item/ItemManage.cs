using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;


public class ItemManage : MonoBehaviour
{
    public float _minItemCooldownTime = 2f;
    public float _maxItemCooldownTime = 4f;
    
    public float _itemInboundRate;
    float _itemGenerateCoolTime;
    float _itemGenerateCooldown = 0;
    //public float _itemSpawnRadius = 4f;
    float _viewportWidth;
    float _viewportHeight;

    GameObject item;


    // Start is called before the first frame update
    void Start()
    {
        //Initializing
        item = Resources.Load("Prefabs/Item") as GameObject;
        _viewportHeight = Camera.main.orthographicSize * (1 - _itemInboundRate);
        _viewportWidth = _viewportHeight * Camera.main.aspect;
        
    }

    // Update is called once per frame
    void Update()
    {
        //When the cooldown time is over
        if (0 < _itemGenerateCooldown)
        {
            _itemGenerateCooldown -= Time.deltaTime;
        }
        else
        {
            //Random cool time
            _itemGenerateCoolTime = Random.Range(_minItemCooldownTime, _maxItemCooldownTime);
            _itemGenerateCooldown = _itemGenerateCoolTime;

            GenerateItem();
        }
    }

    void GenerateItem()
    {
        if (item == null)
        {
            return;
        }
        
        // 랜덤한 위치에 아이템 생성
        Vector3 randomPosition = new(Random.Range(-_viewportWidth, _viewportWidth), Random.Range(-_viewportHeight, _viewportHeight), 0);
        GameObject newItem = Instantiate(item, randomPosition, Quaternion.identity); 
        
    }
}

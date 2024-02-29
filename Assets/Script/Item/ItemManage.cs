using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;


public class ItemManage : MonoBehaviour
{
    public float _minItemCooldownTime = 2f;
    public float _maxItemCooldownTime = 4f;
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
        _viewportHeight = 2 * Camera.main.orthographicSize;
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
        Vector3 randomPosition = new(Random.Range(-_viewportWidth/2, _viewportWidth/2), Random.Range(-_viewportHeight/2, _viewportHeight/2), 0);
        GameObject newItem = Instantiate(item, randomPosition, Quaternion.identity); 
        
    }
}

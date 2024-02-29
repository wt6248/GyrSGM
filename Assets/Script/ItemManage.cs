using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;


public class ItemManage : MonoBehaviour
{
    public float _minItemCooldownTime = 2f;
    public float _maxItemCooldownTime = 4f;
    public float _itemGenerateCoolTime;
    public float _itemGenerateCooldown = 0;
    //public float _itemSpawnRadius = 4f;
    public int _countItem = 0; // for debuging
    public float _viewportWidth;
    public float _viewportHeight;
    public float _screenWidth;
    public float _screenHeight;
    GameObject item;
    Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        //Initializing
        item = Resources.Load("Prefabs/Item") as GameObject;
        mainCamera = Camera.main;
        _viewportWidth = mainCamera.rect.width;
        _viewportHeight = mainCamera.rect.height;
        _screenWidth = Screen.width * _viewportWidth * 3/200;
        _screenHeight = Screen.height * _viewportHeight * 3/200;
        Debug.Log("스크린 크기:"+_screenWidth+","+_screenHeight);
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
        Vector3 randomPosition = new(Random.Range(-_screenWidth, _screenWidth), Random.Range(-_screenHeight, _screenHeight), 0);
        GameObject newItem = Instantiate(item, randomPosition, Quaternion.identity); 
        Debug.Log("아이템 생성 위치" + randomPosition);
        /*
            for debuging(Generate Item)
        */
        _countItem++;
    }


    public void DestroyItem()
    {
        _countItem--;
    }
}

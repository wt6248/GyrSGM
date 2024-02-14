using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    public float _minItemCooldownTime= 2f;
    public float _maxItemCooldownTime = 4f;
    public float _itemGenerateCoolTime;
    public float _itemGenerateCooldown = 0;
    public float _itemSpawnRadius = 4f;
    public int _countItem = 0; // for debuging
    GameObject item;


    // Start is called before the first frame update
    void Start()
    {    
        //Initializing
        item = Resources.Load("Prefabs/Item") as GameObject;  
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
            //Generate item
            GameObject instance = Instantiate(item, _itemSpawnRadius * Random.insideUnitCircle.normalized, Quaternion.identity);
            /*
                for debuging(Generate Item)
            */
            if(instance!=null)
            {
                _countItem++;
                Debug.Log("아이템이 생성됨 : " + _countItem);            
            }
        }
    }

    public void DestroyItem()
    {
        _countItem--;
        Debug.Log("아이템이 사라짐 : " + _countItem);            
    }   
}

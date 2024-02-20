using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    private float speed;
    private float damage;
    //private float attackCooltime;
    //private float current_cooltime;

    bool _canShoot = false;
    
    
    //InvokeRepeating("AutoShoot", 0f, 1.5f);
    
    // Start is called before the first frame update
    void Start()
    {
        // autoshooting function
        StartCoroutine(AutoShootCooldown());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_canShoot) 
        {
            AutoShoot();
        }
    }

    void SetRangeComponent(float _speed, float _damage, float _attackCooltime)
    {
        //TODO 각 변수에 넣기.
    }

    // autoshooting function
    void AutoShoot()
    {
        //TODO
        //플레이어와 적의 상대 위치 체크.
        //총알 instantiate
        //총알 방향 지정.
        //발사하면 _canShoot false로 수정.
    }
    IEnumerator AutoShootCooldown()
    {
        while (true)
        {            
            yield return new WaitForSeconds(1.5f);
            _canShoot = true;
        }
    }

}

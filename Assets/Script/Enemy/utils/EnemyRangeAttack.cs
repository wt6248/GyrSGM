using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    private float speed;
    private float damage;
    private float attackCooltime;
    private float current_cooltime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //쿨타임이면 쿨타임 체크

        //BulletEnemyScatter 총알 발사 가능하면 발사. 
        
    }

    void SetRangeComponent(float _speed, float _damage, float _attackCooltime)
    {
        //TODO. 각 변수에 넣기.
    }
}

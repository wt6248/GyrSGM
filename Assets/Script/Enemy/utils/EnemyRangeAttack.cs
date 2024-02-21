using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    private float speed;
    private float damage;
    //private float attackCooltime;
    //private float current_cooltime;

    public bool _canShoot = false;


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
        if (_canShoot)
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
        Entity player = GameObject.FindObjectOfType<PlayerController>();
        Vector3 displacement = player.transform.position - transform.position;
        //총알 instantiate
        GameObject prefab = Resources.Load("Prefabs/BulletEnemyScatter") as GameObject;
        GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
        BulletScript bullet = instance.GetComponent<BulletScript>();
        //총알 방향 지정.
        bullet._dir = displacement.normalized;
        bullet.Activate(Entity.EntityType.Player);
        EnemyUnitParent enemy = GetComponentInParent<EnemyUnitParent>();
        // bullet.RecordPenetration(enemy);
        //발사하면 _canShoot false로 수정.
        _canShoot = false;
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

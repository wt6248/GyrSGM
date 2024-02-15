using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Status")]
    // damage of bullet
    public uint _damage;
    // speed of bullet
    public float _speed;

    // 총알 개수
    public uint _pelletCount;
    // Spreading angle
    public float _spreadAngle;
    // knockback factor
    public float _knockbackDistance;
    // duration
    public float _duration = 3f;
    public uint _maxPenetration = 1;
    // record entity's id(int)
    public List<int> _penetrationList;
    // bullet direction
    public Vector3 _dir = Vector3.zero;
    // bullet prefab
    public GameObject _bulletPrefab;
    public enum BulletType
    {
        Slug, // sniper
        Scatter, // shotgun
        Rocket // rocket luncher
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = _speed * Time.deltaTime * _dir;
        transform.Translate(movement);
    }

    /*
        적과 충돌했을 때 적의 체력을 깎는 함수를 호출하고 사라짐.
        Rocket Luncher should use its own OnTriggerEnter2D
    */
    virtual public void OnTriggerEnter2D(Collider2D other)
    {
        Entity enemy = other.gameObject.GetComponent<Entity>();
        /*
            enemy.gameObject.GetInstanceID() != enemy.GetInstanceID()
            record in coherent value
        */
        if (_penetrationList.Contains(enemy.gameObject.GetInstanceID()))
        {
            return;
        }
        _penetrationList.Add(enemy.gameObject.GetInstanceID());
        if (other.gameObject.CompareTag("Enemy") && !enemy.IsDead())
        {
            // TODO : item과 머지 이후 PlayerController의 데미지 배율 값을 가져온다.
            // TODO : DecreaseHp로 피해를 줄때 damage*PlayerController의 데미지 배율을 준다.
            enemy.DecreaseHP(_damage);//의 체력깍는 함수 호출
            enemy.Knockback(_dir, _knockbackDistance);

            if (_maxPenetration <= 0)
            {
                Destroy(this.gameObject);
            }
            _maxPenetration -= 1;
        }

    }

    //처음 시작할 때 주어진 속도에 따라 움직이는 코드 작성
    public void SetVelocity(float speed, Vector3 direction)
    {
        _speed = speed;
        _dir = direction.normalized;
    }

    public void SetVelocity(float speed, float eulerAngle)
    {
        _speed = speed;
        float directionX = Mathf.Cos(eulerAngle * Mathf.Deg2Rad);
        float directionY = Mathf.Sin(eulerAngle * Mathf.Deg2Rad);
        _dir = new(directionX, directionY, 0);
    }

    public void Activate()
    {
        if (_duration <= 0)
        {
            _duration = 3f;
        }
        Destroy(this.gameObject, _duration);
    }


}
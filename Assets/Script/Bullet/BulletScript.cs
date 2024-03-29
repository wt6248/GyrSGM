using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Status")]
    // damage of bullet
    public float _damage;
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
        PlayerSlug, // sniper
        PlayerScatter, // shotgun
        PlayerRocket, // rocket luncher
        EnemyScatter
    }
    public Entity.EntityType _attackableType;

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
        Entity entity = other.gameObject.GetComponent<Entity>();
        if (entity._type != _attackableType) { return; }
        if (entity._type == Entity.EntityType.Player && entity.GetComponent<PlayerController>()._isInvincible) { return; }
        if (!CheckPenetration(entity) && !entity.IsDead())
        {
            RecordPenetration(entity);
            AttackEntity(other, _dir);
            if (_maxPenetration < _penetrationList.Count) // -1 for shooter itself
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void RecordPenetration(Entity entity)
    {
        if (!CheckPenetration(entity) && !entity.IsDead())
        {
            _penetrationList.Add(entity.gameObject.GetInstanceID());
        }
    }
    public bool CheckPenetration(Entity entity)
    {
        return _penetrationList.Contains(entity.gameObject.GetInstanceID());
    }

    public void AttackEntity(Collider2D other, Vector3 knockbackDir)
    {
        Entity entity = other.gameObject.GetComponent<Entity>();
        entity.DecreaseHP(_damage);
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyUnitParent>().Knockback(knockbackDir, _knockbackDistance);
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

    public void Activate(Entity.EntityType type)
    {
        if (_duration <= 0)
        {
            _duration = 3f;
        }
        _attackableType = type;
        Destroy(this.gameObject, _duration);
    }

    public void Activate(float speed, Vector3 direction, float damage, float knockbackDistance, float duration, uint maxPenetration, Entity.EntityType attackableType)
    {
        _damage = damage;
        SetVelocity(speed, direction);
        _knockbackDistance = knockbackDistance;
        _duration = duration;
        _maxPenetration = maxPenetration;
        _attackableType = attackableType;
        Destroy(this.gameObject, _duration);
    }
}
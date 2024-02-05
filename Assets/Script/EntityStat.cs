using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStat : MonoBehaviour
{
    public enum EntityType
    {
        Nothing, // null
        Enemy,
        Player,
        Breakable
    }

    // This must be private
    public float _hp;
    public string _name;
    public float _attackDamage;
    public float _speed;
    // what does size mean
    public Vector3 _size;
    public Vector3 _position;
    // ammo inventory
    public int _inventorySize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHP(float delta)
    {
        if (0 < delta)
        {
            _hp += delta;
        }
    }
    public void IncreaseHP(float delta)
    {
        if (0 < delta)
        {
            _hp -= delta;
        }
    }

    public void SetSize(Vector3 size)
    {
        _size = size;
    }
    public void SetPosition(Vector3 position)
    {
        _position = position;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    public void SetAttackDamage(float attackDamage)
    {
        _attackDamage = attackDamage;
    }
    public void SetInventorySize(int inventorySize)
    {
        _inventorySize = inventorySize;
    }


    public bool IsDead()
    {
        if (_hp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

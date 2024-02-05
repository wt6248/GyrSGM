using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Nothing, // null
        Enemy,
        Player,
        Breakable
    }

    // This must be private
    public float _maxHP;
    public float _hp;
    public string _name;
    public float _attackDamage;
    public float _speed;
    // what does size mean
    public Vector3 _size;
    public float _radious;
    // ammo inventory
    public int _inventorySize;
    public EntityType _type;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void DecreaseHP(float delta)
    {
        if (0 < delta)
        {
            _hp -= delta;
        }
    }
    virtual public void IncreaseHP(float delta)
    {
        if (0 < delta)
        {
            _hp += delta;
        }
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

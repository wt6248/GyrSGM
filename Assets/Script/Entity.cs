using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public enum EntityType
    {
        Nothing, // null
        Enemy,
        Player,
        Breakable
    }
    public EntityType _type;

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

    abstract public void DecreaseHP(float delta);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void _DecreaseHP(float delta)
    {
       
        if (0 < delta)
        {
            _hp -= delta;
            Debug.Log("HP: " + _hp);
        }
        if (IsDead())
        {
            Destroy(this.gameObject);
        }
    }
    
    protected void _IncreaseHP(float delta)
    {
        if (0 < delta)
        {
            _hp += delta;
        }
        if (_maxHP < _hp)
        {
            _hp = _maxHP;
        }
    }

    public bool IsDead()
    {
        return _hp <= 0;
    }
}

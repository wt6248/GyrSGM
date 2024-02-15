using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyUnitSpeed : EnemyUnitParent
{
    void Start()
    {
        // init stat
        _type = Entity.EntityType.Enemy;
        //_maxHP = 0.8f;
        _hp = _maxHP;
        _name = "Enemy" + gameObject.GetInstanceID();
        //_attackDamage = 10;
        //_speed = 3;
        _radious = 0.5f; // if chose circle collider
        //_size = new(0.5f, 0.5f, 0f); // if chose box collider


        // init audio
        _hurtSoundSource = gameObject.AddComponent<AudioSource>();
        _hurtSound = Resources.Load<AudioClip>("Audio/dspunch");
        _deadSoundSource = gameObject.AddComponent<AudioSource>();
        _deadSound = Resources.Load<AudioClip>("Audio/dsbgdth1");

        /*
            if enemy has circle collider
        */
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        collider.radius = _radious;
        /*
            if enemy has box collider
        */
        // BoxCollider2D collider = GetComponent<BoxCollider2D>();
        // collider.size = _size;
    }
    void Update()
    {
        Move();
    }
}

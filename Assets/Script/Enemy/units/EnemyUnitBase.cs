using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitBase : EnemyUnitParent
{
    void Start()
    {
        _hp = _maxHP;
        _name = "Enemy" + gameObject.GetInstanceID();
        if (_enemyType == EnemyType.Patrolling) gameObject.AddComponent<EnemyRangeAttack>();

        gameObject.GetComponent<EnemyRangeAttack>().setCatrige("EnemySingle");

        // init audio
        _hurtSoundSource = gameObject.AddComponent<AudioSource>();
        _hurtSound = Resources.Load<AudioClip>("Audio/dspunch");
        _deadSoundSource = gameObject.AddComponent<AudioSource>();
        _deadSound = Resources.Load<AudioClip>("Audio/dsbgdth1");
    }
    void Update()
    {
        Move();
    }
}


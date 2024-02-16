using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitHP : EnemyUnitParent
{
    void Start()
    {
        // unit type
        _enemyType = EnemyType.Charging;
        _hp = _maxHP;
        _name = "Enemy" + gameObject.GetInstanceID();

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